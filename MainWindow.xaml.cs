using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            txtBlock.Text = GetArticle(idUser.Text);
            txtBlock.Text += GetAlbums(idUser.Text);
        }

        private string GetArticle(string idUser)
        {
            WebClient client = new WebClient();
            string reply = client.DownloadString("https://jsonplaceholder.typicode.com/posts?userId=" + idUser);
            ICollection<Article> ListArticle = JsonConvert.DeserializeObject<ICollection<Article>>(reply);
            string result = "";
            foreach (Article article in ListArticle)
            {
                result += "Title : \n" + article.Title + "\n" + "Body : " + article.Body + "\n\n";
                result += GetComments(article.Id);
                result += "\n";
                
            }
            return result;
        }

        public string GetComments(int idArticle)
        {
            string res = "--------------------------------\n";
            WebClient client = new WebClient();
            string reply = client.DownloadString("https://jsonplaceholder.typicode.com/comments?postId=" + idArticle);
            ICollection<Comment> ListComment = JsonConvert.DeserializeObject<ICollection<Comment>>(reply);
            foreach(Comment com in ListComment)
            {
                res += com.name + "\n" + com.email + "\n" + com.body + "\n\n";
            }

            return res;
        }

        private string GetAlbums(string idUser)
        {
            WebClient client = new WebClient();
            string reply = client.DownloadString("https://jsonplaceholder.typicode.com/albums?userId=" + idUser);
            ICollection<Album> ListAlbum = JsonConvert.DeserializeObject<ICollection<Album>>(reply);
            string result = "";
            foreach (Album album in ListAlbum)
            {
                result += "Title : \n" + album.Title + "\n";
                result += GetPictures(album.Id);

            }
            return result;
        }

        public string GetPictures(int idAlbum)
        {
            string res = "";
            WebClient client = new WebClient();
            string reply = client.DownloadString("https://jsonplaceholder.typicode.com/photos?albumId=" + idAlbum);
            ICollection<Picture> ListPictures = JsonConvert.DeserializeObject<ICollection<Picture>>(reply);
            foreach (Picture pic in ListPictures)
            {
                res += pic.Title + "\n" + pic.Url + "\n";
            }

            return res;
        }

    }
}
