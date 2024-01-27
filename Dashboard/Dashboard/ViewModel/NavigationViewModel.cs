﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Dashboard.Model;

namespace Dashboard.ViewModel
{
    class NavigationViewModel: INotifyPropertyChanged
    {
        #region Fields

        // CollectionViewSource enables XAML code to set the commonly used CollectionView properties,
        // passing these settings to the underlying view
        private CollectionViewSource MenuItemsCollection;

        private string filterText;

        private object _selectedViewModel;
        #endregion

        #region Constructor

        public NavigationViewModel()
        {
            // ObservableCollection represents a dynamic data collection data provides notifications
            // when items get added, removed, or when the whole list is refreshed
            ObservableCollection<MenuItems> menuItems = new ObservableCollection<MenuItems>
            {
                new MenuItems { MenuName = "Home", MenuImage = @"Assets/Home_Icon.png" },
                new MenuItems { MenuName = "Desktop", MenuImage = @"Assets/Desktop_Icon.png" },
                new MenuItems { MenuName = "Documents", MenuImage = @"Assets/Document_Icon.png" },
                new MenuItems { MenuName = "Downloads", MenuImage = @"Assets/Download_Icon.png" },
                new MenuItems { MenuName = "Pictures", MenuImage = @"Assets/Images_Icon.png" },
                new MenuItems { MenuName = "Music", MenuImage = @"Assets/Music_Icon.png" },
                new MenuItems { MenuName = "Movies", MenuImage = @"Assets/Movies_Icon.png" },
                new MenuItems { MenuName = "Trash", MenuImage = @"Assets/Trash_Icon.png" }
            };

            MenuItemsCollection = new CollectionViewSource { Source = menuItems };
            MenuItemsCollection.Filter += MenuItems_Filter;
        }
        #endregion

        #region Properties

        // ICollectionView enables collections to have the functionalities of the current record management,
        // custom sorting, filtering and grouping
        public ICollectionView SourceCollection => MenuItemsCollection.View;

        public string FilterText
        {
            get => filterText;
            set
            {
                filterText = value;
                MenuItemsCollection.View.Refresh();
                OnPropertyChanged(nameof(FilterText));
            }
        }

        public object SelectedViewModel
        {
            get => _selectedViewModel;
            set { _selectedViewModel = value; OnPropertyChanged(nameof(SelectedViewModel)); }
        }
        #endregion

        #region Methods

        public void SwitchViews(object parameter)
        {
            switch (parameter)
            {
                case "Home":
                    SelectedViewModel = new HomeViewModel();
                    break;
                case "Desktop":
                    SelectedViewModel = new DesktopViewModel();
                    break;
                case "Documents":
                    SelectedViewModel = new DocumentViewModel();
                    break;
                case "Downloads":
                    SelectedViewModel = new DownloadViewModel();
                    break;
                case "Pictures":
                    SelectedViewModel = new PictureViewModel();
                    break;
                case "Music":
                    SelectedViewModel = new MusicViewModel();
                    break;
                case "Movies":
                    SelectedViewModel = new MovieViewModel();
                    break;
                case "Trash":
                    SelectedViewModel = new TrashViewModel();
                    break;
                default:
                    SelectedViewModel = new HomeViewModel();
                    break;
            }
        }

        private void MenuItems_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                e.Accepted = true;
                return;
            }

            MenuItems _item = e.Item as MenuItems;
            if (_item != null && _item.MenuName.ToUpper().Contains(FilterText.ToUpper()))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }
        #endregion

        #region INotifyPropertyChanged elements

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion
    }
}
