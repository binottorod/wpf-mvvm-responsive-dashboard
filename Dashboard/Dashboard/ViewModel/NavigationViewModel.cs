﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Dashboard.Model;

namespace Dashboard.ViewModel
{
    public class NavigationViewModel: INotifyPropertyChanged
    {
        // CollectionViewSource enables XAML code to set the commonly used CollectioView properties,
        // passing these settings to the underlying view
        private CollectionViewSource MenuItemsCollection;


        // ICollectionView enables collections to have the functionalities of the current record management,
        // custom sorting, filtering and grouping
        public ICollectionView SourceCollection => MenuItemsCollection.View;

        public NavigationViewModel()
        {
            // ObservableCollection represents a dynamic data collection data provides notifications
            // when items get added, removed, or when the whole list is refreshed
            ObservableCollection<MenuItems> menuItems = new ObservableCollection<MenuItems>
            {
                new MenuItems { MenuName = "Home", MenuImage = "@Assets/Home_Icon.png"},
                new MenuItems { MenuName = "Desktop", MenuImage = "@Assets/Desktop_Icon.png"},
                new MenuItems { MenuName = "Documents", MenuImage = "@Assets/Document_Icon.png"},
                new MenuItems { MenuName = "Downloads", MenuImage = "@Assets/Download_Icon.png"},
                new MenuItems { MenuName = "Pictures", MenuImage = "@Assets/Images_Icon.png"},
                new MenuItems { MenuName = "Music", MenuImage = "@Assets/Music_Icon.png"},
                new MenuItems { MenuName = "Movies", MenuImage = "@Assets/Movies_Icon.png"},
                new MenuItems { MenuName = "Trash", MenuImage = "@Assets/Trash_Icon.png"}
            };

            MenuItemsCollection = new CollectionViewSource { Source = menuItems };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
