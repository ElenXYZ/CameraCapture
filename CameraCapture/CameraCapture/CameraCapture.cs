using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CameraCapture
{
    public partial class CameraCapture : Form
    {
        #region Camera Capture Variables
        private Capture _capture = null; //Camera
        private bool _captureInProgress = true; //Variable to track camera state
        #endregion

        public CameraCapture()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_capture != null)
            {
                if (_captureInProgress)
                {
                    //stop the capture
                    btnStart.Text = "Start Capture"; //Change text on button
                   
                    //Because this is a static event, you must detach your event handlers when your application is disposed, or memory leaks will result.
                    Application.Idle -= new EventHandler(ProcessFrame); //detach the idle event handler
                    _captureInProgress = false; //Flag the state of the camera
                }
                else
                {                    
                    btnStart.Text = "Stop"; //Change text on button                  
                    Application.Idle += new EventHandler(ProcessFrame); //attach or re-attach the idle event handler
                    _captureInProgress = true; //Flag the state of the camera
                }

            }
            else
            {
                _capture = new Capture(0);
                _captureInProgress = false;
                btnStart_Click(null, null);
            }
           
        }

        private void ProcessFrame(object sender, EventArgs arg)
        {
            ImageViewer viewer = new ImageViewer(); //create an image viewer
            //Capture capture = new Capture(); //create a camera captue
            viewer.Image = _capture.QueryFrame();  //line 1
            captureBox.Image = viewer.Image.Bitmap;  //line 2
            //***If you want to access the image data the use the following method call***/
            //Image<Bgr, Byte> frame = new Image<Bgr,byte>(_capture.RetrieveBgrFrame().ToBitmap());
            //Image<Bgr, Byte> frame = _capture.QueryFrame().ToImage<Bgr, byte>(); //alternativly RetrieveBgrFrame() works in the same manner
            //                                                //As the Application.Idle thread belongs to this form we no longer need to invoke the picturebox as with the _capture.ImageGrabbed event method
            //captureBox.Image = frame.ToBitmap();
            //if (RetrieveBgrFrame.Checked)
            //{
            //    Image<Bgr, Byte> frame = _capture.QueryFrame(); //alternativly RetrieveBgrFrame() works in the same manner
            //    //As the Application.Idle thread belongs to this form we no longer need to invoke the picturebox as with the _capture.ImageGrabbed event method
            //    captureBox.Image = frame.ToBitmap();

            //}
            //else if (RetrieveGrayFrame.Checked)
            //{
            //    Image<Gray, Byte> frame = _capture.QueryGrayFrame();//alternativly .RetrieveGrayFrame(); works in the same manner
            //    //As the Application.Idle thread belongs to this form we no longer need to invoke the picturebox as with the _capture.ImageGrabbed event method
            //    captureBox.Image = frame.ToBitmap();
            //}
            //else if (QuerySmallFrame.Checked)
            //{
            //    Image<Bgr, Byte> frame = _capture.QuerySmallFrame();
            //    captureBox.Image = frame.ToBitmap();
            //}
        }
    }
}
