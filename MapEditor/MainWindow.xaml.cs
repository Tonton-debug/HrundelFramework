using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.Win32;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using ProjectLibrary;
namespace MapEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Project _mainProject = new Project();
        private string _entityCharacteristicsForRendering="";
        private string _selectedSceneName = "";
        public MainWindow()
        {
            InitializeComponent();
            TypeEntity typeEntity = TypeEntity.PhysicalEntity;
            foreach (var item in Enum.GetValues(typeEntity.GetType()))
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = item;
                EntityTypeComboBox.Items.Add(comboBoxItem);
            }
        }
        
        private void SceneWindowsFormsHost_Initialized(object sender, EventArgs e)
        {

        }

        private void SceneGLControl_Load(object sender, EventArgs e)
        {

        }
        
        private void SceneGLControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
        //    SceneWindowsFormsHost.Visibility = Visibility.Visible;
        //    WindowsFormsHostEntity.Visibility = Visibility.Hidden;
            GL.ClearColor(new Color4(0.6f, 0.6f, 0.6f, 1f));
            GL.Viewport(0, 0, SceneGLControl.Width, SceneGLControl.Height);
                 GL.Clear(ClearBufferMask.ColorBufferBit);
            if (_selectedSceneName != "")
            {
                foreach (var pref in _mainProject.Scenes[_selectedSceneName].PrefabEntities)
                {
                    if (!_mainProject.EntitiesCharacteristics.ContainsValue(pref.Value.MyEntityCharacteristics)|| pref.Value.MyEntityCharacteristics==null)
                    {
                        DeletePrefab(pref.Key);
                        continue;
                    }
                    pref.Value.Rendering(_mainProject.Scenes[_selectedSceneName].MyCamera.GetOrthoMatrix());
                }
            }
            SceneGLControl.SwapBuffers();

        }

        private void WindowsFormsHostEntity_Initialized(object sender, EventArgs e)
        {
          
        }

        private void EntityGLControl_Load(object sender, EventArgs e)
        {
            GL.ClearColor(new Color4(0.1f, 0.2f, 0.5f, 1f));
           
        }

        private void EntityGLControl_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
     //       SceneWindowsFormsHost.Visibility = Visibility.Hidden;
    //        WindowsFormsHostEntity.Visibility = Visibility.Visible;
            GL.Viewport(0, 0, EntityGLControl.Width, EntityGLControl.Height);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            if (_entityCharacteristicsForRendering != "")
                _mainProject.EntitiesCharacteristics[_entityCharacteristicsForRendering].Rendering(new Camera(2).GetOrthoMatrix(), Vector2.Zero, new Vector2(1,1));
            EntityGLControl.SwapBuffers();


        }
        private void AddMap(string name, string size)
        {
            try
            {
                _mainProject.Scenes.Add(name, new Scene(new Camera(float.Parse(size), true)));
                AddMapToPanel(name);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось создать карту\n" + ex.Message);
            }
        }
        private void AddMapToPanel(string name)
        {
            try
            {
                Button button = new Button();
                button.Click += MapButton_Click;
                button.Content = name;
                MapsWrapPanel.Children.Add(button);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Не удалось создать карту\n"+ ex.Message);
            }
        }
        private void CreateMapButton_Click(object sender, RoutedEventArgs e)
        {
            ClearPrefSettings();
            MapSettings mapSettings = new MapSettings();
            if (mapSettings.ShowDialog() == true)
            {
                AddMap(mapSettings.MapNameTextBox.Text,mapSettings.SizeCameraTextBox.Text);
               
            }
        }

        private void MapButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedSceneName = (sender as Button).Content.ToString();
            PrefabsStackPanel.Children.Clear();
            foreach (var pref in _mainProject.Scenes[_selectedSceneName].PrefabEntities)
                AddPrefabToPanel(pref.Key);
            
            SceneGLControl.Invalidate();
        }

        private void DeleteMapButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _mainProject.Scenes.Remove(_selectedSceneName);
               
                List<UIElement> uIElements=new List<UIElement>();
                PrefabsStackPanel.Children.Clear();
                for (int i = 0; i < MapsWrapPanel.Children.Count; i++)
                {
                    if ((MapsWrapPanel.Children[i] as Button).Content.ToString() == _selectedSceneName)
                    {
                        _mainProject.Scenes.Remove(_selectedSceneName);
                        _selectedSceneName = "";

                        MapsWrapPanel.Children.RemoveAt(i);
                        if (i - 1 >= 0)
                            MapButton_Click(MapsWrapPanel.Children[i - 1], null);
                        else
                            SceneGLControl.Invalidate();
                        break;
                    }
                       
                }
              
                
            }
            catch
            {
                MessageBox.Show("Не удалось удалить карту");
            }
        }
        private void AddEntity(string name)
        {
            _mainProject.EntitiesCharacteristics.Add(name, new EntityCharacteristics(name));
            AddEntityToPanel(name);
        }
        private void AddEntityToPanel(string name)
        {
            Button button = new Button();
            button.Click += EntityButton_Click;
            button.Content = name;
            EntitiesListBox.Items.Add(button);
        }
        private void CreateEntityButton_Click(object sender, RoutedEventArgs e)
        {
            NameWindow entityNameWindow = new NameWindow("Название сущности:");
            if (entityNameWindow.ShowDialog() == true)
            {
                try
                {
                    AddEntity(entityNameWindow.NameTextBox.Text);
                }
                catch
                {
                    MessageBox.Show("Не удалось создать сущность");
                }
            }
        }

        private void EntityButton_Click(object sender, RoutedEventArgs e)
        {
            
            Button button = sender as Button;
            _entityCharacteristicsForRendering = button.Content.ToString();
            RColor.Text = _mainProject.EntitiesCharacteristics[_entityCharacteristicsForRendering].MyColor.R.ToString().Replace(',','.');
            GColor.Text = _mainProject.EntitiesCharacteristics[_entityCharacteristicsForRendering].MyColor.G.ToString().Replace(',', '.');
            BColor.Text = _mainProject.EntitiesCharacteristics[_entityCharacteristicsForRendering].MyColor.B.ToString().Replace(',', '.');
            NameEntityTextBox.Text = _entityCharacteristicsForRendering;
            EntityTypeComboBox.SelectedIndex = (int)_mainProject.EntitiesCharacteristics[_entityCharacteristicsForRendering].MyType;
            EntityGLControl.Invalidate();

        }

        private void SaveEntitySettingsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Color4 colorF = new Color4(float.Parse(RColor.Text.Replace('.',',')), float.Parse(GColor.Text.Replace('.', ',')), float.Parse(BColor.Text.Replace('.', ',')),1);
               
                _mainProject.EntitiesCharacteristics[_entityCharacteristicsForRendering].MyColor=colorF;
                TypeEntity typeEntity=TypeEntity.SolidEntity;
               _mainProject.EntitiesCharacteristics[_entityCharacteristicsForRendering].MyType= (TypeEntity)Enum.Parse(typeEntity.GetType(),(EntityTypeComboBox.SelectedItem as ComboBoxItem).Content.ToString());
                EntityGLControl.Invalidate();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка");
            }
           
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MainTabControl.SelectedIndex == 0)
            {
                WindowsFormsHostEntity.Visibility = Visibility.Hidden;
                WindowsFormsHostEntity.IsEnabled = false;
                SceneWindowsFormsHost.Visibility = Visibility.Visible;
                SceneWindowsFormsHost.IsEnabled = true;
                SceneGLControl.MakeCurrent();
            }
            else
            {
                WindowsFormsHostEntity.Visibility = Visibility.Visible;
                WindowsFormsHostEntity.IsEnabled = true;
                SceneWindowsFormsHost.Visibility = Visibility.Hidden;
                SceneWindowsFormsHost.IsEnabled = false;
                EntityGLControl.MakeCurrent();
            }
            

        }
        private void AddPrefabToPanel(string namePref)
        {
            try
            {
                Button button = new Button();
                button.Content = namePref;
                button.Click += PrefabButton_Click;
                PrefabsStackPanel.Children.Add(button);
                SceneGLControl.Invalidate();
            }
            catch
            {
                MessageBox.Show("Не удалось добавить сущность");
            }
        }
        private void AddPrefab(string namePref, EntityCharacteristics entityCharacteristics)
        {
            try
            {
                PrefabEntity prefabEntity = new PrefabEntity(entityCharacteristics);
                _mainProject.Scenes[_selectedSceneName].PrefabEntities.Add(namePref, prefabEntity);
                AddPrefabToPanel(namePref);
            }
            catch
            {
                MessageBox.Show("Не удалось добавить сущность");
            }
        }
        private void AddPrefabButton_Click(object sender, RoutedEventArgs e)
        {
            NameWindow nameWindow = new NameWindow("Название префаба:");
            string namePref;
            if (nameWindow.ShowDialog() == true)
            {
                namePref = nameWindow.NameTextBox.Text;
                nameWindow = new NameWindow("Название сущности:");
                if(nameWindow.ShowDialog() == true)
                {
                    AddPrefab(namePref, _mainProject.EntitiesCharacteristics[nameWindow.NameTextBox.Text]);
                   
                }
            }
        }

        private void PrefabButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedSceneName == "")
                return;
            NamePrefTextBlock.Text = (sender as Button).Content.ToString();
            XPositionOrdinateTextBox.Text = _mainProject.Scenes[_selectedSceneName].PrefabEntities[(sender as Button).Content.ToString()].Position.X.ToString().Replace(',', '.');
            YPositionOrdinateTextBox.Text = _mainProject.Scenes[_selectedSceneName].PrefabEntities[(sender as Button).Content.ToString()].Position.Y.ToString().Replace(',', '.');
            XScaleOrdinateTextBox.Text = _mainProject.Scenes[_selectedSceneName].PrefabEntities[(sender as Button).Content.ToString()].Scale.X.ToString().Replace(',', '.');
            YScaleOrdinateTextBox.Text = _mainProject.Scenes[_selectedSceneName].PrefabEntities[(sender as Button).Content.ToString()].Scale.Y.ToString().Replace(',', '.');
        }

        private void SavePrefSettings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedSceneName == "")
                    return;
                _mainProject.Scenes[_selectedSceneName].PrefabEntities[NamePrefTextBlock.Text].Position = new Vector2(float.Parse(XPositionOrdinateTextBox.Text.Replace('.', ',').ToString()), float.Parse(YPositionOrdinateTextBox.Text.Replace('.', ',').ToString()));
                _mainProject.Scenes[_selectedSceneName].PrefabEntities[NamePrefTextBlock.Text].Scale = new Vector2(float.Parse(XScaleOrdinateTextBox.Text.Replace('.', ',').ToString()), float.Parse(YScaleOrdinateTextBox.Text.Replace('.', ',').ToString()));
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить изменения");
            }
            SceneGLControl.Invalidate();
        }

        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Universal hrundel project |*.uhp";
            if (saveFileDialog.ShowDialog() == true)
            {
                _mainProject.Save(saveFileDialog.FileName);
            }
        }

        private void LoadFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Universal hrundel project |*.uhp";
            if (openFileDialog.ShowDialog() == true)
            {
             _mainProject= Project.Load(openFileDialog.FileName);
                MapsWrapPanel.Children.Clear();
                ClearPrefSettings();
                PrefabsStackPanel.Children.Clear();
                EntitiesListBox.Items.Clear();
                foreach (var scene in _mainProject.Scenes)            
                    AddMapToPanel(scene.Key);
                foreach (var entity in _mainProject.EntitiesCharacteristics)
                    AddEntityToPanel(entity.Key);
            }
        }
        private void DeleteEntity(string name)
        {
            for (int i = 0; i < EntitiesListBox.Items.Count; i++)
            {
                if ((EntitiesListBox.Items[i] as Button).Content.ToString() == name)
                {
                    if(name==_entityCharacteristicsForRendering)
                        ClearEntitySettings();
                    EntitiesListBox.Items.RemoveAt(i);
                    _mainProject.EntitiesCharacteristics.Remove(name);
                    EntityGLControl.Invalidate(); 
                }
            }
        }
        private void DeletePrefab(string name)
        {
            for (int i = 0; i < PrefabsStackPanel.Children.Count; i++)
            {
                if ((PrefabsStackPanel.Children[i] as Button).Content.ToString() == name)
                {
                    if(name== NamePrefTextBlock.Text)
                        ClearPrefSettings();
                    PrefabsStackPanel.Children.RemoveAt(i);
                    
                    _mainProject.Scenes[_selectedSceneName].PrefabEntities.Remove(name);
                    SceneGLControl.Invalidate();
                 
                }
            }
        }
        private void DeletePrefabButton_Click(object sender, RoutedEventArgs e)
        {
            DeletePrefab(NamePrefTextBlock.Text);
          
        }
        private void ClearPrefSettings()
        {
            NamePrefTextBlock.Text = "";
            XPositionOrdinateTextBox.Text = "";
            YPositionOrdinateTextBox.Text = "";
            XScaleOrdinateTextBox.Text = "";
            YScaleOrdinateTextBox.Text = "";
        }
        private void ClearEntitySettings()
        {
            NameEntityTextBox.Text = "";
            RColor.Text = "";
            GColor.Text = "";
            BColor.Text = "";
           
        }
        private void DeleteEntityButton_Click(object sender, RoutedEventArgs e)
        {
            _entityCharacteristicsForRendering = "";
            DeleteEntity(NameEntityTextBox.Text);
           
        }

        private void ExportFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Hrundel game resources |*.hgr";
            if (saveFileDialog.ShowDialog() == true)
            {
                _mainProject.SaveToResourse(saveFileDialog.FileName);
            }
        }
    }
}
