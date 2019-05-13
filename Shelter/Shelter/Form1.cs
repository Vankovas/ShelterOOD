using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shelter
{
    public partial class Form1 : Form
    {
        private Panel activePanel;
        private Panel lastPanel;
        private Shelter shelter;
        List<string> items;
        public Form1()
        {
            shelter = new Shelter("Zin Zan Min - Chinese restaurant", "do pichka si g1zina u levo", "69420360noscope", "hapniku4e@chingchong.com");
            dummyInit();
            
            InitializeComponent();
            activePanel = panelOverview;
            generateOverview();
        }

        private void btnOverview_Click(object sender, EventArgs e)
        {
            ChangeViews(panelOverview);
        }

        private void ChangeViews(Panel panel)
        {
            if(activePanel != panel)
            {
                activePanel.Visible = false;
                lastPanel = activePanel;
                activePanel = panel;
                activePanel.Visible = true;
            }
        }

        private void btnAnimals_Click(object sender, EventArgs e)
        {
            ChangeViews(panelAnimals);
        }

        private void btnOwners_Click(object sender, EventArgs e)
        {
            ChangeViews(panelOwners);
        }

        private void dummyInit()
        {
            shelter.AddAnimal(new Dog("qk pishonqk", new DateTime(2018, 6, 13), "na pichka si g1zina", new RFIDTag("12345")));
            shelter.AddAnimal(new Dog("poserko", new DateTime(2028, 2, 23), "na pichka si g1zina i nego", new RFIDTag("14234")));

            shelter.AddOwner(new Owner("123123", "gulibuli"));
            shelter.AddOwner(new Owner("31241", "buliguli"));

            shelter.AdoptAnimal("123123", new RFIDTag("12345"));
        }

        private void lbAnimals_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.lbAnimals.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                RFIDTag tempTag = new RFIDTag(lbAnimals.Items[index].ToString().Split(' ')[1]);
                this.updateAnimal(shelter.GetAnimalInfo(tempTag));

                this.ChangeViews(panelAnimalProfile);
            }
        }

        private string splitter(string stringToSplit, Object collection)
        {
            return "";
        }

        private void labelBack_MouseClick(object sender, MouseEventArgs e)
        {
            this.ChangeViews(lastPanel);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.ChangeViews(panelAddAnimal);
        }

        private void btnWalk_Click(object sender, EventArgs e)
        {
            RFIDTag tempTag = new RFIDTag(tbRFID.Text);
            shelter.WalkDog(tempTag);
            this.updateAnimal(shelter.GetAnimalInfo(tempTag));
        }

        private void updateAnimal(string info)
        {
            Dictionary<string, string> properties = splitter(info);

            tbRFID.Text = properties["RFID"];
            tbAnimalType.Text = properties["Type"].Split('.')[1];
            tbDateFound.Text = properties["Date brought"];
            tbLocationFound.Text = properties["Location found"];
            tbInShelter.Text = properties["In shelter"];
            tbDescription.Text = properties["Description"];
            tbIsAdoptable.Text = properties["Adoptable"];
            tbLastOwner.Text = shelter.GetOwnerByAnimal(new RFIDTag(properties["RFID"]));

            btnAdopt.Enabled = properties["Adoptable"].Equals("True");
            btnClaim.Enabled = shelter.GetOwnerByAnimal(new RFIDTag(properties["RFID"])) != null && properties["In shelter"].Equals("True");

            if (properties["Type"].Contains("Dog"))
            {
                labelCatSpecial.Visible = false;
                if (properties.ContainsKey("Last date walked"))
                {
                    tbSpecial.Text = properties["Last date walked"];
                }

                labelLastWalked.Visible = true;
                btnWalk.Enabled = properties["In shelter"].Equals("True");
            }
            else
            {
                labelLastWalked.Visible = false;
                btnEditSpecial.Visible = true;
                if (properties.ContainsKey("Extra Info"))
                {
                    tbSpecial.Text = properties["Extra Info"]; 
                }
                tbSpecial.Visible = true;
                btnWalk.Enabled = false;
            }
        }

        private void updateOwner(string info)
        {
            lbOwnersAnimals.Items.Clear();

            Dictionary<string, string> properties = splitter(info);

            tbOwnerId.Text = properties["Card ID"];
            tbOwnerName.Text = properties["Last name"];

            foreach (string str in shelter.GetAnimalsByOwner(properties["Card ID"]))
            {
                string[] animalsSplitted = str.Split(',');
                string temp = "";

                for (int i = 0; i < 3; i++)
                {
                    temp += animalsSplitted[i];
                }
                lbOwnersAnimals.Items.Add(temp);
            }
        }

        private void btnAdopt_Click(object sender, EventArgs e)
        {
            Form form2 = new Form2();

            foreach (string str in shelter.GetOwnersInfo())
            {
               ((ListBox)form2.Controls[0]).Items.Add(str);
            }

            if (form2.ShowDialog(this) == DialogResult.OK)
            {
                string cardId = ((ListBox)form2.Controls[0]).SelectedItem.ToString().Split(',')[0].Split(':')[1].Trim();

                int price = shelter.AdoptAnimal(cardId, new RFIDTag(tbRFID.Text));
                if (price != -1)
                {
                    MessageBox.Show("Successfull! Adoption fee: " + price);
                }
                else
                {
                    MessageBox.Show("Animal with this RFID already exists!");
                }
            }

            this.updateAnimal(shelter.GetAnimalInfo(new RFIDTag(tbRFID.Text)));
        }

        private void btnClaim_Click(object sender, EventArgs e)
        {
            Form form2 = new Form2();

            ((ListBox)form2.Controls[0]).Items.Add(shelter.GetOwnerByAnimal(new RFIDTag(tbRFID.Text)));

            if (form2.ShowDialog(this) == DialogResult.OK)
            {
                string cardId = ((ListBox)form2.Controls[0]).SelectedItem.ToString().Split(',')[0].Split(':')[1].Trim();

                MessageBox.Show("Successfull!" + shelter.ClaimAnimal(cardId, new RFIDTag(tbRFID.Text)));
            }
        }

        private void lbOwners_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.lbOwners.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                string cardId = lbOwners.Items[index].ToString().Split(',')[0].Split(':')[1].Trim();
                this.updateOwner(shelter.GetOwnerInfo(cardId));

                this.ChangeViews(panelOwnerProfile);
            }
        }

        private void lbOwnersAnimals_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = this.lbOwnersAnimals.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                RFIDTag tempTag = new RFIDTag(lbOwnersAnimals.Items[index].ToString().Split(' ')[1]);
                this.updateAnimal(shelter.GetAnimalInfo(tempTag));

                this.ChangeViews(panelAnimalProfile);
            }
        }

        private void radioDog_MouseClick(object sender, MouseEventArgs e)
        {
            tbAddSpecial.Enabled = false;
        }

        private void radioCat_MouseClick(object sender, MouseEventArgs e)
        {
            tbAddSpecial.Enabled = true;
        }

        private void btnAddConfirm_Click(object sender, EventArgs e)
        {
            if(radioDog.Checked)
            {
                shelter.AddAnimal(new Dog(tbAddDescription.Text, pickDateFound.Value, tbAddLocation.Text, new RFIDTag(tbAddRFID.Text)));
            }
            else if(radioCat.Checked)
            {
                shelter.AddAnimal(new Cat(tbAddDescription.Text, pickDateFound.Value, tbAddLocation.Text, new RFIDTag(tbAddRFID.Text), tbAddSpecial.Text));
            }

            foreach(Control ctrl in panelAddAnimal.Controls)
            {
                if(ctrl is TextBox)
                {
                    ((TextBox)ctrl).Text = "";
                }
            }
            MessageBox.Show("Success!");
        }

        private void btnAddOwnerConfirm_Click(object sender, EventArgs e)
        {
            shelter.AddOwner(new Owner(tbAddCardId.Text, tbAddName.Text));
            MessageBox.Show("Success!");
        }

        private void btnAddOwner_Click(object sender, EventArgs e)
        {
            ChangeViews(panelAddOwner);
        }

        private Dictionary<string, string> splitter(string info)
        {
            string[] splitted = info.Split(',');
            Dictionary<string, string> properties = new Dictionary<string, string>();

            foreach (string str in splitted)
            {
                str.Trim();
                string[] temp = str.Split(':');
                properties.Add(temp[0].Trim(), temp[1].Trim());
            }

            return properties;
        }

        private void Search(TextBox tb, ListBox lb)
        {
            lb.Items.Clear();
            foreach (Object item in items)
            {
                if (!tb.Text.Equals(""))
                {
                    if (item.ToString().Contains(tb.Text))
                    {
                        lb.Items.Add(item.ToString());
                    }
                }
                else
                {
                    lb.Items.Add(item.ToString());
                }
            }
        }

        private void initItems(ListBox lb)
        {
            items = new List<string>();
            foreach (Object item in lb.Items)
            {
                items.Add(item.ToString());
            }
        }

        private void tbSearchOwner_KeyUp(object sender, KeyEventArgs e)
        {
            this.Search(tbSearchOwner, lbOwners);
        }

        private void tbSearchAnimal_KeyUp(object sender, KeyEventArgs e)
        {
            this.Search(tbSearchAnimal, lbAnimals);
        }

        private void generateOverview()
        {
            labelAdopted.Text = shelter.GenerateOverview()[0];
            labelAnimalsInShelter.Text = shelter.GenerateOverview()[1];
            labelProudOwners.Text = shelter.GenerateOverview()[2];
        }

        private void panelOverview_VisibleChanged(object sender, EventArgs e)
        {
            if(((Panel)sender).Visible)
            {
                generateOverview();
            }
        }

        private void panelAnimals_VisibleChanged(object sender, EventArgs e)
        {
            if(((Panel)sender).Visible)
            {
                lbAnimals.Items.Clear();
                foreach (string str in shelter.GetAnimalsInfo())
                {
                    string[] splitted = str.Split(',');
                    string temp = "";

                    for (int i = 0; i < 3; i++)
                    {
                        temp += splitted[i];
                    }
                    lbAnimals.Items.Add(temp);
                }
                this.initItems(lbAnimals);
            }
        }

        private void panelOwners_VisibleChanged(object sender, EventArgs e)
        {
            lbOwners.Items.Clear();
            foreach (string str in shelter.GetOwnersInfo())
            {

                lbOwners.Items.Add(str);
            }

            this.initItems(lbOwners);
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            Form formRFID = new Form3("Enter RFID: ");
            RFIDTag tempTag = null;

            formRFID.ShowDialog();
            if (formRFID.DialogResult == DialogResult.OK)
            {
                foreach(Control ctrl in formRFID.Controls)
                {
                    if(ctrl is TextBox && ((TextBox)ctrl).Text != "")
                    {
                        tempTag = new RFIDTag(((TextBox)ctrl).Text);
                        if(shelter.GetAnimalInfo(tempTag) != null)
                        {
                            if (shelter.BackToShelter(tempTag))
                            {
                                Form formLocation = new Form3("Enter location found: ");

                                formLocation.ShowDialog();

                                if (formLocation.DialogResult == DialogResult.OK)
                                {
                                    foreach (Control control in formLocation.Controls)
                                    {
                                        if (control is TextBox)
                                        {
                                            shelter.ChangeLocation(tempTag, ((TextBox)control).Text);
                                        }
                                    }
                                }
                            }
                            this.updateAnimal(shelter.GetAnimalInfo(tempTag));
                            ChangeViews(panelAnimalProfile);
                        }
                        else
                        {
                            tbAddRFID.Text = ((TextBox)ctrl).Text;
                            ChangeViews(panelAddAnimal);
                        }
                    }
                }
            }
        }

        private void btnEditSpecial_Click(object sender, EventArgs e)
        {
            if (tbAnimalType.Text.Equals("Cat"))
            {
                shelter.EditCatDesc(new RFIDTag(tbRFID.Text), tbSpecial.Text);
            }
        }
    }
}
