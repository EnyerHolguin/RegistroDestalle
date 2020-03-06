using Personaas.BLL;
using Personaas.Entidades;
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

namespace Personaas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Personas persona = new Personas();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext =  persona;

        }
            private void CargarGrid()
            {
            this.DataContext = null;
            this.DataContext = persona;

            }
            private void Limpiar()
            {

                IdTextBox.Text = "0";
                NombreTextBox.Text = string.Empty;
                DireccionTextBox.Text = string.Empty;
                CedulaTextBox.Text = string.Empty;
                FechaNacimientoDatePicker.SelectedDate = DateTime.Now;
                DetalleDatagridView.ItemsSource = new List<TelefonosDetalle>();

                
            }

        private bool Validar()
        {
            bool paso = true;

            if (string.IsNullOrWhiteSpace(IdTextBox.Text))
                paso = false;
            else
            {
                try
                {
                    int i = Convert.ToInt32(IdTextBox.Text);
                }
                catch (FormatException)
                {
                    paso = false;
                }
            }

            if (string.IsNullOrWhiteSpace(NombreTextBox.Text))
                paso = false;
            else
            {
                foreach (var caracter in NombreTextBox.Text)
                {
                    if (!char.IsLetter(caracter))
                        paso = false;
                }
            }

            

            if (paso == false)
                MessageBox.Show("El Datos es invalidos");

            return paso;
        }



        private void AgregarButton_Click(object sender, RoutedEventArgs e)
            {
            persona.Telefono.Add(new TelefonosDetalle(Telefonotextbox.Text, TipoTextBox.Text));
                CargarGrid();

                Telefonotextbox.Clear();
                TipoTextBox.Clear();

                Telefonotextbox.Focus();


            }

            

            private void NuevoButton_Click(object sender, RoutedEventArgs e)
            {
                Limpiar();
            }

            private void GuardarButton_Click(object sender, RoutedEventArgs e)
            {
                
                bool paso = false;

                if (!Validar())
                    return;

                if(persona.PersonaId == 0)
            {
                paso = PersonasBLL.Guardar(persona);
            }
                else
            {
                if (ExisteEnLaBaseDeDAto())
                {
                    paso = PersonasBLL.Modificar(persona);
                }
                else
                {
                    MessageBox.Show("No se puede modificar la persona que no existe");
                    return;
                }
            }
                if(paso)
            {
                Limpiar();
                MessageBox.Show("Guardado");
            }
            else
            {
                MessageBox.Show("No se pudo guardar");
            }
        }
            private bool ExisteEnLaBaseDeDAto()
            {
                Personas personaAnterior = PersonasBLL.Buscar(persona.PersonaId);
                return (persona != null);

            }


            private void EliminarButton_Click(object sender, RoutedEventArgs e)
            {
        
                if (PersonasBLL.Eliminar(persona.PersonaId))
                {
                    MessageBox.Show("Eliminado");
                }
                else
                {
                    
                        MessageBox.Show("si no existe no se puede eliminar");
                }
            }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Personas personaAnterior = PersonasBLL.Buscar(persona.PersonaId);

            if (persona != null)
            {
                persona = personaAnterior;
                CargarGrid();
            }
            else
            {
                MessageBox.Show("No encontrado");
            }
         }

        private void Removerbutton_Click(object sender, RoutedEventArgs e)
        {
            if(DetalleDatagridView.Items.Count > 1 && DetalleDatagridView.SelectedIndex < DetalleDatagridView.Items.Count - 1 )
            {
                persona.Telefono.RemoveAt(DetalleDatagridView.SelectedIndex);
                CargarGrid();
            }
        }




    }
    }

