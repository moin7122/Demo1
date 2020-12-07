using FlickrNet;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using WpfApp1.Command;

namespace WpfApp1.View_Model
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public List<PhotoPath> PhotoPathList { get; set; }

        public string SearchString { get; set; }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(SearchCommandExecute, null);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SearchCommandExecute(object parameter)
        {
            const string apiKey = "37c9f47fd84e5905b7cb7c6e98037d92";

            Flickr flickr = new Flickr(apiKey);

            var options = new PhotoSearchOptions
            {
                Tags = SearchString,
                PerPage = 20,
                Page = 1
            };

            PhotoCollection photos = flickr.PhotosSearch(options);
            var listOfUrls = photos.Select(x => x.MediumUrl).ToList();
            PhotoPathList = new List<PhotoPath>();
            listOfUrls.ForEach(
                x =>
                {
                    PhotoPathList.Add(new PhotoPath
                    {
                        Path = x
                    });
                });
            RaisePropertyChanged("PhotoPathList");
        }      

        public void RaisePropertyChanged(string property)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
             
    }

    public class PhotoPath
    {
        public string Path { get; set; }
    }
}