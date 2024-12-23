using System;
using System.Text;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace WinFormsXml
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGenerar_Click(object sender, EventArgs e)//evento del click del boton generar 
        {
            string textoDelLabel = label1.Text;
            if (textoDelLabel != "")
            {
                // MessageBox.Show($"El texto del labeles: {textoDelLabel}");//mostramos la ruta leida 

                // Ruta del archivo XML
                string path = textoDelLabel;

                // metodo para cargar el archivo XML
                UsingXmlDocumentUpdateInformation(path);
            }
            else { MessageBox.Show($"Seleccione archivo XML "); }
        }






        private void btnLeer_Click(object sender, EventArgs e) // evento click del boton leer
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK) //si exite un archivo muestra la ruta 
            {
                label1.Text = openFileDialog1.FileName;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string textoDelLabel = label1.Text;
            if (textoDelLabel != "")
            {
                // Ruta del archivo XML
                string path = textoDelLabel;

                // metodo para cargar el archivo XML
                UsingXmlDocumentSearchforInformation(path);
            }
            else { MessageBox.Show($"Seleccione archivo XML "); }
        }



        private static void UsingXmlDocumentSearchforInformation(string path)//resivimos la ruta del xml
        {

            // Cargar el archivo XML
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);



            /*
             *A continuación, seleccionamos un nodo es este caso los del proveedor 
             *Luego  buscamos los datos específicos como por ejemplo el nombre del proveedor 
             *
             */

            // Definir el namespace manager para manejar los prefijos en el XML
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            nsmgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

            // Obtener el nodo del proveedor y lo guardamos en supplierNode (nodo del proveedor )
            XmlNode supplierNode = xmlDoc.SelectSingleNode("//cac:AccountingSupplierParty/cac:Party", nsmgr);

            if (supplierNode != null)
            {
                // Obtener el nombre de la empresa, supplierNode: Representa el nodo del proveedor (<cac:Party>) que fue seleccionado previamente. 
                XmlNode nameNode = supplierNode.SelectSingleNode("cac:PartyName/cbc:Name", nsmgr);//SelectSingleNode: Este método busca un nodo hijo dentro del nodo actual que coincida con la ruta
                string supplierName = nameNode?.InnerText ?? "Nombre no encontrado";//Si el nodo <cbc:Name> existe, se almacena en nameNode. Si no existe, nameNode será null.


                // Obtener la dirección
                XmlNode addressNode = supplierNode.SelectSingleNode("cac:PhysicalLocation/cac:Address/cac:AddressLine/cbc:Line", nsmgr);
                string supplierAddress = addressNode?.InnerText ?? "Dirección no encontrada";


                // Mostrar los datos en cuadros de mensaje
                MessageBox.Show($"Nombre del proveedor: {supplierName}", "Información del Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show($"Dirección del proveedor: {supplierAddress}", "Información del Proveedor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No se encontró información del proveedor.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private static void UsingXmlDocumentUpdateInformation(string path)
        {

            // Cargar el archivo XML
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            // Definir el namespace manager para manejar los prefijos en el XML
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            nsmgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

            // Buscar el nodo del NIT del proveedor
            XmlNode nitNode = xmlDoc.SelectSingleNode("//cac:AccountingSupplierParty/cac:Party/cac:PartyTaxScheme/cbc:CompanyID", nsmgr);

            if (nitNode != null)
            {
                // Mostrar el valor actual
                string oldNit = nitNode.InnerText;
                MessageBox.Show($"Valor actual del NIT: {oldNit}");

                // Modificar el valor
                string newNit = "123456789"; // Nuevo NIT
                nitNode.InnerText = newNit;

                MessageBox.Show($"Nuevo valor del NIT: {newNit}");

                // Guardar los cambios en el archivo
                xmlDoc.Save(path + "_Modificado.xml");
                MessageBox.Show("El archivo XML ha sido actualizado y guardado.");
            }
            else
            {
                MessageBox.Show("No se encontró el nodo del NIT del proveedor.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
