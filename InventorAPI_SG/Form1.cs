using Inventor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace InventorAPI_SG
{
    public partial class Form1 : Form
    {
        private Inventor.Application inventorApplication;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Connecting and opening Autodesk Inventor.
                inventorApplication = (Inventor.Application)System.Activator.CreateInstance(System.Type.GetTypeFromProgID("Inventor.Application")) as Inventor.Application;
                inventorApplication.Visible = true;
                MessageBox.Show("Connected Successfully.");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Error connecting to Inventor: " + ex.Message);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Create a document
            PartDocument docPart = (PartDocument)inventorApplication.Documents.Add(DocumentTypeEnum.kPartDocumentObject, inventorApplication.FileManager.GetTemplateFile(DocumentTypeEnum.kPartDocumentObject));

            // Definition of Document created
            PartComponentDefinition partDefinition = docPart.ComponentDefinition;

            //Adding plan
            PlanarSketch sketch = partDefinition.Sketches.Add(partDefinition.WorkPlanes[3]);
            //Settin up the Geometry
            TransientGeometry geometry = inventorApplication.TransientGeometry;
            //Defining some points
            Point2d point1 = geometry.CreatePoint2d(20, 40);
            Point2d point2 = geometry.CreatePoint2d(30, 60);
            Point2d centerPoint = geometry.CreatePoint2d(50, 50);

            // Sketching the shape
            SketchCircle circle = sketch.SketchCircles.AddByCenterRadius(centerPoint, 10);

            Profile profile = sketch.Profiles.AddForSolid();

            // Defining extrusion
            ExtrudeDefinition extrudeDefinition = partDefinition.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile, PartFeatureOperationEnum.kJoinOperation);

            extrudeDefinition.SetDistanceExtent(100, PartFeatureExtentDirectionEnum.kNegativeExtentDirection);

            // Extrude feature
            ExtrudeFeature extrudeFeature = partDefinition.Features.ExtrudeFeatures.Add(extrudeDefinition);

        }


    }
}
