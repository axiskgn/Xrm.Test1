using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using Xrm.Test1.DataModel.Dictionary;
using Xrm.Test1.DataModel.Entity;
using Xrm.Test1.DataModel.SimpleFactory;
using Xrm.Test1.DbRepository.MsAccess.Common;
using Xrm.Test1.Merge.SimpleEngine;
using Xrm.Test1.RawData.E1;
using Xrm.Test1.RawData.E1.BaseJsonConverter;
using Xrm.Test1.Utils.DataGetter;
using Xrm.Test1.WpfUi.Common;
using Xrm.Test1.WpfUi.Controls;
using Xrm.Test1.WpfUi.Controls.Download;
using Xrm.Test1.WpfUi.Controls.FilterPanel;

namespace Xrm.Test1.WpfUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private MsAccesRootRepository _rootRepository;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            _rootRepository = new MsAccesRootRepository(Path.GetFullPath("base.mdb"), new DictionaryFactory(),
                new PersonFactory(), new ResumeSourceInfoFactory(), new ResumeFactory());

            var ra = _rootRepository.GetReadAcces();

            var resumeListModel = new ResumeListModel();
            var filterModel = new FilterPanelModel(ra);
            resumeListModel.SetList(ra.ResumeRepository.GetAll());
            var listControl = new ResumeLists { DataContext = resumeListModel };
            resumeListModel.DownloadCmd = new RelayCommand(delegate(object o)
            {
                #region 

                var downloadModel = new DownloadModel();
                downloadModel.Progress = 0;
                downloadModel.CloseCmd = new RelayCommand(delegate(object o1)
                {
                    filterModel.LoadData();
                    resumeListModel.SetList(ra.ResumeRepository.GetAll());
                    _mainControl.Content = listControl;
                });

                var downloadControl = new DownloadView {DataContext = downloadModel};
                _mainControl.Content = downloadControl;

                var thread = new Thread(delegate()
                {
                    var mergeEngine = new SimpleMergeEngine(_rootRepository);
                    //TestDb();
                    var dataSource = new E1RawDataSources(new BaseConverter(), new HttpDataGetter(), new E1UrlCreator());
                    dataSource.Start();

                    var counter = 0;

                    var mergeTime = new TimeSpan();
                    var downloadTime = new TimeSpan();
                    var startTime = DateTime.Now;
                    while (true)
                    {
                        var startDownloadTime = DateTime.Now;
                        var dataList = dataSource.GetNextDataBlock();
                        var endDownloadTime = DateTime.Now;
                        downloadTime += endDownloadTime - startDownloadTime;
                        if (dataList == null)
                        {
                            break;
                        }

                        foreach (var resumeRaw in dataList)
                        {
                            counter++;
                            var startDateMerge = DateTime.Now;
                            mergeEngine.AddResume(resumeRaw, "E1.RU");
                            var endDateMerge = DateTime.Now;
                            mergeTime += endDateMerge - startDateMerge;
                            if (counter == 10)
                            {
                                Dispatcher.BeginInvoke(new Action(delegate
                                {
                                    downloadModel.Progress++;
                                    downloadModel.AllTime = endDateMerge - startTime;
                                    downloadModel.MergeTime = mergeTime;
                                    downloadModel.DownloadTime = downloadTime;
                                    downloadModel.NewPersonCount = mergeEngine.NewPersonCount;
                                    downloadModel.NewResumeCount = mergeEngine.NewResumeCount;
                                }));
                                counter = 0;
                            }
                            //Console.WriteLine(@"{0} - {1} - {2}", counter, resumeRaw.Fio, endDateMerge - startDateMerge);
                        }

                    }


                });
                thread.Start();

                #endregion
            });

            #region Фильтрация в потоке

            var isNeedUpdate = false;
            Thread updateThread = null;

            filterModel.PropertyChanged += delegate(object o, PropertyChangedEventArgs args)
            {
                if (updateThread == null)
                {
                    updateThread = new Thread(delegate()
                    {
                        do
                        {
                            isNeedUpdate = false;
                            var newList = CreateNewList(filterModel, ra.ResumeRepository.GetAll());
                            Dispatcher.BeginInvoke(new Action(delegate
                            {
                                resumeListModel.SetList(newList);
                            }));
                        } while (isNeedUpdate);
                        updateThread = null;
                    });
                    updateThread.Start();
                }
                else
                {
                    isNeedUpdate = true;
                }
            };

            #endregion

            var filterPanelView = new FilterPanelView{DataContext = filterModel};
            resumeListModel.FilterPanel = filterPanelView;

            var detailModel = new ResumeDetailModel();
            detailModel.Back += delegate
            {
                _mainControl.Content = listControl;
            };

            var detailControl = new ResumeDetailView();

            resumeListModel.SelectedResume+= delegate(IResume resume)
            {
                detailModel.SetResume(resume);
                detailControl.DataContext = null;
                detailControl.DataContext = detailModel;
                _mainControl.Content = detailControl;
            };

            _mainControl.Content = listControl;

        }

        /// <summary>
        /// Примитивная фильтрация
        /// </summary>
        /// <param name="filterModel"></param>
        /// <param name="getAll"></param>
        /// <returns></returns>
        private IList<IResume> CreateNewList(FilterPanelModel filterModel, IEnumerable<IResume> getAll)
        {
            var newList = getAll;

            if (filterModel.IsHeader)
            {
                newList = newList.Where(t => t.Header.Contains(filterModel.HeaderFiler));
            }

            if (filterModel.IsCity && filterModel.CityFilter is ICity)
            {
                var city = filterModel.CityFilter as ICity;
                newList = newList.Where(t => t.City.Id == city.Id);
            }

            if (filterModel.IsEducation && filterModel.EducationFilter is IEducation)
            {
                var education = filterModel.EducationFilter as IEducation;
                newList = newList.Where(t => t.Education.Id == education.Id);
            }

            if (filterModel.IsWorkingType && filterModel.WorkingTypeFilter is IWorkingType)
            {
                var workingType = filterModel.WorkingTypeFilter as IWorkingType;
                newList = newList.Where(t => t.WorkingType.Id == workingType.Id);
            }

            if (filterModel.IsScheduleWork && filterModel.ScheduleWorkFilter is IScheduleWork)
            {
                var scheduleWork = filterModel.ScheduleWorkFilter as IScheduleWork;
                newList = newList.Where(t => t.ScheduleWork.Id == scheduleWork.Id);
            }

            return newList.ToList();
        }
    }
}
