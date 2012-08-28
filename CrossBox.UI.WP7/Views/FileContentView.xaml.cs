using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Cirrious.MvvmCross.WindowsPhone.Views;
using CrossBox.Core.ViewModels;
using Microsoft.Phone.Controls;

namespace CrossBox.UI.WP7.Views
{
    public partial class FileContentView : BaseFileContentView
    {
        public FileContentView()
        {
            InitializeComponent();
        }
    }

    public class BaseFileContentView : MvxPhonePage<FileContentViewModel>
    {}
}