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
        public List<TelefonosDetalle> Detalle { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.Detalle = new List<TelefonosDetalle>();

        }
            private void CargarGrid()
            {
                DetalleDatagridView.ItemsSource = null;
                DetalleDatagridView.ItemsSource = this.Detalle;

            }
            private void Limpiar()
            {

                IdTextBox.Text = "0";
                NombreTextBox.Text = string.Empty;
                DireccionTextBox.Text = string.Empty;
                CedulaTextBox.Text = string.Empty;
                FechaNacimientoDatePicker.SelectedDate = DateTime.Now;

                this.Detalle = new List<TelefonosDetalle>();
                CargarGrid();
            }

            private Personas LlenaClase()
            {
                Personas persona = new Personas();
                persona.PersonaId = Convert.ToInt32(IdTextBox.Text);
                persona.Nombre = NombreTextBox.Text;
                persona.Direccion = DireccionTextBox.Text;
                persona.Cedula = CedulaTextBox.Text;
                persona.FechaNacimiento = FechaNacimientoDatePicker.DisplayDate;

                persona.Telefono = this.Detalle;

                return persona;
            }

            private void LlenaCampos(Personas persona)
            {
                IdTextBox.Text = Convert.ToString(persona.PersonaId);
                NombreTextBox.Text = persona.Nombre;
                DireccionTextBox.Text = persona.Nombre;
                CedulaTextBox.Text = persona.Nombre;
                FechaNacimientoDatePicker.DisplayDate = persona.FechaNacimiento;

                this.Detalle = persona.Telefono;
                CargarGrid();
            }

            private bool Validar()
            {
                bool paso = true;

                if (IdTextBox.Text == string.Empty)
                {
                    MessageBox.Show("El campo no puede estar vacio");
                    IdTextBox.Focus();
                    paso = false;
                }
                if (string.IsNullOrWhiteSpace(DireccionTextBox.Text))
                {
                    MessageBox.Show("El campo no puede estar vacio");
                    DireccionTextBox.Focus();
                    paso = false;
                }
                if (string.IsNullOrWhiteSpace(CedulaTextBox.Text))
                {
                    MessageBox.Show("El campo no puede estar vacio");
                    CedulaTextBox.Focus();
                    paso = false;
                }
                if (this.Detalle.Count == 0)
                {
                    MessageBox.Show( "Debe agregar algun telefono");
                    TelefonoTextBox.Focus();
                }

                return paso;
            }

            private void AgregarButton_Click(object sender, RoutedEventArgs e)
            {
                if (DetalleDatagridView.DataContext != null)
                    this.Detalle = (List<TelefonosDetalle>)DetalleDatagridView.DataContext;
                this.Detalle.Add(
                new TelefonosDetalle(
                    Id: 0,
                    Telefono: Convert.ToInt32(TelefonoTextBox.Content),
                    Tipo: TipoTextBox.Text
                      )
                    );
                CargarGrid();
                TelefonoTextBox.Focus();
                TipoTextBox.Clear();


            }

            private void Remover_TextChanged(object sender, TextChangedEventArgs e)
            {
                if (DetalleDatagridView.Row.Count > 0 && DetalleDatagridView.CurrentColumn != null)
                {
                    Detalle.RemoveAt(DetalleDatagridView.CurrentColumn.DisplayIndex);
                    CargarGrid();
                }

            }

            private void NuevoButton_Click(object sender, RoutedEventArgs e)
            {
                Limpiar();
            }

            private void GuardarButton_Click(object sender, RoutedEventArgs e)
            {
                Personas persona;
                bool paso = false;
                if (!Validar())
                    return;

                persona = LlenaClase();
                if (Convert.ToInt32(IdTextBox.Text) == 0)
                {
                    paso = PersonasBLL.Guardar(persona);
                    MessageBox.Show("Guardado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No fue posible Guardar!!", " Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            private bool ExisteEnLaBaseDeDAto()
            {
                Personas persona = PersonasBLL.Buscar(Convert.ToInt32(IdTextBox.Text));
                return (persona != null);

            }




            private void EliminarButton_Click(object sender, RoutedEventArgs e)
            {
                int id;
                int.TryParse(IdTextBox.Text, out id);

                Limpiar();
                if (!ExisteEnLaBaseDeDAto())
                {
                    MessageBox.Show("No se puede eliminar un prestamo inexistente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (PersonasBLL.Eliminar(id))
                        MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }

