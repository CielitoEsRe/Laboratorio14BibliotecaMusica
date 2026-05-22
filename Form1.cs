using Laboratorio14BibliotecaMusica.Clases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Laboratorio14BibliotecaMusica
{
    public partial class Form1 : Form
    {
        List<Album> listaAlbumes = new List<Album>();

        string archivo = "albumes.json";

        List<Cancion> cancionesTemp = new List<Cancion>();

        int indiceEditar = -1;

        public Form1()
        {
            InitializeComponent();

            CargarDatos(); ;
        }

        private void buttonAgregarCancion_Click(object sender, EventArgs e)
        {
            Cancion nueva = new Cancion();

            nueva.Nombre = txtCancion.Text;
            nueva.Artista = txtArtista.Text;
            nueva.Duracion = txtDuracion.Text;

            cancionesTemp.Add(nueva);

            listBoxCanciones.Items.Add(nueva.Nombre);

            txtCancion.Clear();
            txtDuracion.Clear();
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            Album nuevo = new Album();

            nuevo.Titulo = txtTitulo.Text;
            nuevo.ArtistaAlbum = txtArtista.Text;
            nuevo.FechaPublicacion = dateTimePickerFecha.Value;
            nuevo.Canciones = cancionesTemp;

            listaAlbumes.Add(nuevo);

            GuardarDatos();

            MostrarDatos();

            Limpiar();
        }

        private void MostrarDatos()
        {
            dataGridViewAlbumes.DataSource = null;

            dataGridViewAlbumes.DataSource = listaAlbumes.Select(a => new
            {
                a.Titulo,
                a.ArtistaAlbum,
                Fecha = a.FechaPublicacion.ToShortDateString(),
                CantidadCanciones = a.Canciones.Count
            }).ToList();
        }

        private void GuardarDatos()
        {
            string json = JsonConvert.SerializeObject(listaAlbumes, Newtonsoft.Json.Formatting.Indented);

            File.WriteAllText(archivo, json);
        }

        private void CargarDatos()
        {
            if (File.Exists(archivo))
            {
                string json = File.ReadAllText(archivo);

                listaAlbumes = JsonConvert.DeserializeObject<List<Album>>(json);

                MostrarDatos();
            }
        }

        private void Limpiar()
        {
            txtTitulo.Clear();
            txtArtista.Clear();
            txtCancion.Clear();
            txtDuracion.Clear();

            cancionesTemp = new List<Cancion>();

            listBoxCanciones.Items.Clear();
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            string artista = txtBuscar.Text.ToLower();

            dataGridViewAlbumes.DataSource = null;

            dataGridViewAlbumes.DataSource = listaAlbumes
                .Where(a => a.ArtistaAlbum.ToLower().Contains(artista))
                .Select(a => new
                {
                    a.Titulo,
                    a.ArtistaAlbum,
                    Fecha = a.FechaPublicacion.ToShortDateString(),
                    CantidadCanciones = a.Canciones.Count
                }).ToList();
        }

        private void buttonEditar_Click(object sender, EventArgs e)
        {
            if (dataGridViewAlbumes.CurrentRow != null)
            {
                indiceEditar = dataGridViewAlbumes.CurrentRow.Index;

                txtTitulo.Text = listaAlbumes[indiceEditar].Titulo;

                txtArtista.Text = listaAlbumes[indiceEditar].ArtistaAlbum;

                dateTimePickerFecha.Value = listaAlbumes[indiceEditar].FechaPublicacion;

                cancionesTemp = listaAlbumes[indiceEditar].Canciones;

                listBoxCanciones.Items.Clear();

                foreach (var c in cancionesTemp)
                {
                    listBoxCanciones.Items.Add(c.Nombre);
                }

                listaAlbumes.RemoveAt(indiceEditar);

                GuardarDatos();

                MostrarDatos();
            }
    }

    }
}