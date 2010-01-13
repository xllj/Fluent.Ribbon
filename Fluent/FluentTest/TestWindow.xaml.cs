using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Fluent;

namespace FluentTest
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class TestWindow : RibbonWindow
    {
        public TestWindow()
        {
            InitializeComponent();
            ScreenTip.HelpPressed += new EventHandler<ScreenTipHelpEventArgs>(OnScreenTipHelpPressed);
           
            Loaded += delegate
            {
                tabGroup1.Visibility = System.Windows.Visibility.Visible;
                tabGroup2.Visibility = System.Windows.Visibility.Visible;
            };
        }
        static AdornerLayer GetAdornerLayer(UIElement element)
        {
            UIElement current = element;
            while (true)
            {
                current = (UIElement)VisualTreeHelper.GetParent(current);
                if (current is AdornerDecorator) return AdornerLayer.GetAdornerLayer((UIElement)VisualTreeHelper.GetChild(current, 0));
            }
        }
        void OnScreenTipHelpPressed(object sender, ScreenTipHelpEventArgs e)
        {
            System.Diagnostics.Process.Start((string)e.HelpTopic);
        }

        private void OnLauncherButtonClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Launcher button pressed!!!");
        }

        private void OnBtnClick(object sender, RoutedEventArgs e)
        {
            if (tabGroup1.Visibility == Visibility.Visible)
            {
                tabGroup1.Visibility = System.Windows.Visibility.Collapsed;
                tabGroup2.Visibility = System.Windows.Visibility.Collapsed;
               // tabGroup3.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                tabGroup1.Visibility = System.Windows.Visibility.Visible;
                tabGroup2.Visibility = System.Windows.Visibility.Visible;
                //tabGroup3.Visibility = System.Windows.Visibility.Visible;
            }
            e.Handled = true;
        }

        private void OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            /*UIElement control = QuickAccessItemsProvider.PickQuickAccessItem((sender as RibbonTabControl), e.GetPosition(sender as RibbonTabControl));
            if (control != null)
            {               
                if(control is CheckBox)
                {
                    (control as CheckBox).Width = 100;
                    (control as CheckBox).Height = 22;
                }
                if (quickAccessToolbar.Items.Contains(control)) quickAccessToolbar.Items.Remove(control);
                else quickAccessToolbar.Items.Add(control);
            }            */
        }

        private void OnTestClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void OnSomeClick(object sender, RoutedEventArgs e)
        {

        }

        public static RoutedCommand CustomRoutedCommand = new RoutedCommand("lala",typeof(TestWindow));

        private void ExecutedCustomCommand(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Custom Command Executed");
            canEx = !canEx;
        }

        private bool canEx = true;
        private void CanExecuteCustomCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;// canEx;
        }

        private void OnSomeTestClick(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("lala");
            CheckLogicalTree(ribbon);
            logicalTreeView.Items.Clear();
            BuildLogicalTree(ribbon, null);
        }

        void CheckLogicalTree(DependencyObject root)
        {
            var children = LogicalTreeHelper.GetChildren(root);
            foreach (var child in children)
            {
                if (child is DependencyObject)
                {
                    if (LogicalTreeHelper.GetParent(child as DependencyObject) != root)
                    {
                        Debug.WriteLine(string.Format("Incorrect logical parent in element - {0} in {1}", child.ToString(), root.ToString()));
                    }
                    CheckLogicalTree(child as DependencyObject);
                }
            }
        }

        void BuildLogicalTree(DependencyObject root, TreeViewItem item)
        {
            TreeViewItem newItem = new TreeViewItem();
            newItem.Header = String.Format("[{0}]{1}", root.ToString(), (root is FrameworkElement)?(root as FrameworkElement).Name:"");
            if(item!=null)
            {
                item.Items.Add(newItem);
            }
            else
            {
                logicalTreeView.Items.Add(newItem);
            }
            var children = LogicalTreeHelper.GetChildren(root);
            foreach (var child in children)
            {
                if (child is DependencyObject)
                {
                    BuildLogicalTree(child as DependencyObject, newItem);
                }
            }
        }

        private void OnSilverClick(object sender, RoutedEventArgs e)
        {
            ThemesManager.SetTheme(this,Themes.Office2010Silver);            
        }

        private void OnBlackClick(object sender, RoutedEventArgs e)
        {
            ThemesManager.SetTheme(this, Themes.Office2010Black);
        }

        private void OnSplitClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Split Clicked!!!");
        }

        private void OnUnfreezeClick(object sender, RoutedEventArgs e)
        {
            Clipboard.IsSnapped = false;
        }

        private void OnFreezeClick(object sender, RoutedEventArgs e)
        {
            Clipboard.IsSnapped = true;
        }
    }

}