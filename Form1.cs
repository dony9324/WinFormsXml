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



        private static void UsingXmlDocumentSearchforInformation(string path)//resibimos la ruta del xml
        {

            // Cargar el archivo XML
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);



            /*
             *A continuación, seleccionamos un nodo es este caso los del proveedor 
             *Luego  buscamos los datos específicos como por ejemplo el nombre del proveedor 
             *
             *
             * XML tiene nodos con prefijos (como cac y cbc), que están relacionados con espacios de nombres específicos. 
             *Sin un XmlNamespaceManager, el programa no sabe a qué espacio de nombres pertenece cada prefijo, y
             *no podría encontrar los nodos al hacer las consultas XPath.
             */





            // Definir el namespace manager para manejar los prefijos en el XML
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");// cac (Common Aggregate Components) Representa componentes agregados comunes, es decir, elementos que agrupan otros nodos.
                        nsmgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");//(Common Basic Components)Representa componentes básicos comunes, como texto, números, fechas, etc.

                        // Obtener el nodo del proveedor y lo guardamos en supplierNode (nodo del proveedor )
                        XmlNode supplierNode = xmlDoc.SelectSingleNode("//cac:AccountingSupplierParty/cac:Party", nsmgr);// //cac:AccountingSupplierParty: Busca cualquier nodo AccountingSupplierParty que esté bajo el espacio de nombres cac. /cac:Party: Busca el nodo hijo Party bajo el espacio de nombres cac.

            if (supplierNode != null)
            {
                // Obtener el nombre de la empresa, supplierNode: Representa el nodo del proveedor (<cac:Party>) que fue seleccionado previamente. 
                XmlNode nameNode = supplierNode.SelectSingleNode("cac:PartyName/cbc:Name", nsmgr);//SelectSingleNode: Este método busca un nodo hijo dentro del nodo actual que coincida con la ruta        Uso del nsmgr: El XmlNamespaceManager se pasa como argumento para que el parser entienda que cac corresponde al espacio de nombres especificado.
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



            /*
             * Aquí están los namespaces definidos en el archivo:
             * 
             xmlns="urn:oasis:names:specification:ubl:schema:xsd:Invoice-2"
             xmlns:cac="urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2"
             xmlns:cbc="urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2"
             xmlns:ds="http://www.w3.org/2000/09/xmldsig#"
             xmlns:ext="urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2"
             xmlns:sts="dian:gov:co:facturaelectronica:Structures-2-1"
             xmlns:xades="http://uri.etsi.org/01903/v1.3.2#"
             xmlns:xades141="http://uri.etsi.org/01903/v1.4.1#"
             xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
             */

            nsmgr.AddNamespace("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            nsmgr.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            nsmgr.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
            nsmgr.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            nsmgr.AddNamespace("sts", "dian:gov:co:facturaelectronica:Structures-2-1");
            nsmgr.AddNamespace("xades", "http://uri.etsi.org/01903/v1.3.2#");
            nsmgr.AddNamespace("xades141", "http://uri.etsi.org/01903/v1.4.1#");




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
