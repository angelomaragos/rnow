using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using api.rightnow.com;
using obj.api.rightnow.com;
using System.Windows.Forms;

namespace Example2___Update
{

    
    public partial class frmMain : Form
    {
        
        #region Instance fields

        private const String SITE_URL = "https://dymo--tst.custhelp.com/cgi-bin/dymo.cfg/php/xml_api/soap_api.php";
        private const String CONNECT_USER = "rnow";
        private const String CONNECT_PASSWORD = "password123";
        private RNOWObjectFactory _objectFactory;
        private int inc_id;
        #endregion

        public frmMain()
        {
            InitializeComponent();
            initGUI();
            this.Disposed += new EventHandler(frmMain_Disposed); 
        }

        void frmMain_Disposed(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void  IncidentUpdater()
        {
            //Create the RNOWObjectFactory and login
            _objectFactory = new RNOWObjectFactory(SITE_URL);
            bool loggedIn = _objectFactory.login(CONNECT_USER, CONNECT_PASSWORD);

            if (loggedIn == false)
            {
                throw new Exception("Error logging in with Connect Data integration. Check site url, username & password.");
            }
        }
        private void UpdateIncident(int incId, string Trackingnumber)
        {
            //First fetch the incident we want to update
            RNOWIncident incident = new RNOWIncident(incId);
            incident = (RNOWIncident)_objectFactory.get(incident);

            if (incident == null)
            {
                lstResults.Items.Add ("Incidnet with id: " + incId + " was not found.");
                return;
            }
            /*
            //Set the new subject on the incident object
            incident.Subject = subject;

            //Create a new list for newly appended threads.
            //Do not try and add the new thread directly by doing incident.Threads.Add(newThread)
            List<RNOWThread> threads = new List<RNOWThread>();
            threads.Add(incThread);
            incident.Threads = threads;

            //Send the updated incident object back to RightNow
            _objectFactory.update(incident);*/
            // declare the incident object
            RNOWIncident IncObj = new RNOWIncident();

            // delcare the custom field objects
            RNOWCfVal CfObj = new RNOWCfVal();
            List<RNOWCfVal> CfsObj = new List<RNOWCfVal>();
            IncObj = new RNOWIncident();
            CfsObj = new List<RNOWCfVal>();
            IncObj.ID = incId;
            _objectFactory.get(IncObj);

            // get the custom fields
            CfsObj = IncObj.CustomField;

            // update the custom fields
            CfsObj = new List<RNOWCfVal>();
            // init cf obj, set a TEXT custom field, add it to the cf list
            CfObj = new RNOWCfVal();
            CfObj.CfId = 67;
            CfObj.ValStr = Trackingnumber;
            CfsObj.Add(CfObj);
            // set the incident custom field property
            IncObj.CustomField = CfsObj;

            // update the incident
            lstResults.Items.Add ("If you see a value of 1, the Incident updated successfully: " + _objectFactory.update(IncObj));


            // print the custom fields
            lstResults.Items.Add ("Custom Field Count: " + CfsObj.Count);
            for (int i = 0; i < CfsObj.Count; i++)
            {
                lstResults.Items.Add ("*** CUSTOM FIELD " + i);
                lstResults.Items.Add ("CfId:     " + CfsObj[i].CfId);
                lstResults.Items.Add ("DataType: " + CfsObj[i].DataType);
                // lstResults.Items.Add ("ValInt:   " + CfsObj[i].ValInt);
                lstResults.Items.Add ("The RightNow incident has been updated:   " + CfsObj[i].ValStr);
            }
            // lstResults.Items.Add ("ValTime:  " + CfsObj[i].ValTime);

        }
        public void DisplayIncident(int incId)
        {
            //First fetch the incident we want to update
            RNOWIncident incident = new RNOWIncident(incId);
            incident = (RNOWIncident)_objectFactory.get(incident);


            lstResults.Items.Add ("\n Incident Id: " + incId);
            lstResults.Items.Add ("Incidnet Tracking number : " + incident.Subject);

            List<RNOWThread> threads = incident.Threads;

            foreach (RNOWThread thread in threads)
            {
                lstResults.Items.Add  ("\n--------------------Begin Thread Data--------------------");
                lstResults.Items.Add  ("Thread Sequence: " + thread.Seq);
                lstResults.Items.Add ("Thread Note: " + thread.Note);
                lstResults.Items.Add ("Thread Entered Date: " + thread.Entered);
                lstResults.Items.Add ("--------------------End Thread Data--------------------\n");
            }
        }

        private int Collectrefno()
        {

            while (true)
            {
                
                String refStr = txtReferenceNo.Text.Trim(); 

                try
                {
                    //  Console.Write("ddd");
                    //   lstResults.Items.Add ("jasnfckjsan " + refStr);
                    // RNOWObjectFactory OF = new RNOWObjectFactory("https://dymo--tst.custhelp.com/cgi-bin/dymo.cfg/php/xml_api/soap_api.php");
                    object[][] res = _objectFactory.Search(RNOWUtil.SearchType.INCIDENTS, (int)RNOWUtil.SearchIncident.REF_NO, RNOWUtil.SearchOperator.EQUALS, refStr);
                    //  lstResults.Items.Add ("ajnd" + res);
                    int inc_id = -9999;
                    if (res != null && res.Length > 0 && res[0] != null && res[0].Length > 0)
                    {
                        try
                        {
                            inc_id = int.Parse(res[0][0].ToString());
                            lstResults.Items.Add ("incident ID: " + inc_id);
                        }
                        catch (Exception ex)
                        {
                           MessageBox.Show(  ("Integer Parse Error:" + ex.Message + Environment.NewLine + ex.StackTrace),"An Error Has Occured");
                        }
                    }
                    // we can stipulate 0 for the first index (the row) since searching // for a ref_no should only return one row. // we can use a 0 for the second index (the column) since the Search() // method returns the record id in the first column of each row. if (res != null && res.Length > 0 && res[0] != null && res[0].Length > 0) { try { inc_id = int.Parse(res[0][0].ToString()); } catch (Exception ex) { lstResults.Items.Add ("Integer Parse Error:" + ex.Message + Environment.NewLine + ex.StackTrace); } }
                    //refno = Int32.Parse(refStr);
                    return inc_id;
                }
                catch (Exception)
                {
                    MessageBox.Show(("Invalid reference provided."));
                  
                }
            }



            // select i_id from incidents where ref_no = refno;

        }

        private string CollectIncidentTracking()
        {
            System.Console.Write("Please enter the tracking number associated with this RightNow Reference Number: ");
            return System.Console.ReadLine();
        }
        
        private void initGUI()
        {
            
            txtReferenceNo.Enabled = true;
            txtTrackingNo.Enabled = false;
            lblIncidentNo.Text = "Incident No:";
            btnVerify.Enabled = true;
            btnDisplayResults.Enabled = false;
            lstResults.Items.Clear();
            btnReset.Enabled = false;
            txtReferenceNo.Text = "";
            txtTrackingNo.Text = "";
            txtReferenceNo.Focus(); 
 
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (_objectFactory == null)
            {
                IncidentUpdater();
            }
            inc_id=Collectrefno();
            if ( inc_id != -9999)
            {
                lblIncidentNo.Text  = "Incident No:" + inc_id.ToString();
                txtTrackingNo.Enabled = true;
                txtTrackingNo.Focus();
                btnVerify.Enabled = false;
                btnDisplayResults.Enabled = true;
                btnReset.Enabled = true;
            }
        }

        private void btnDisplayResults_Click(object sender, EventArgs e)
        {
            UpdateIncident(inc_id, txtTrackingNo.Text);
            DisplayIncident(inc_id);
            btnDisplayResults.Enabled = false;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            initGUI();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
