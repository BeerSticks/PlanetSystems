using PlanetSystem.Data;
using PlanetSystem.Models.Bodies;
using PlanetSystem.Models.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneralUI
{
    public partial class Form1 : Form
    {
        PlanetarySystem currentPlanetarySystem;
        AstronomicalBody selectedBody;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCreateNewGroup();


            listboxOfPlanetarySystems.Width = planetarySystemTreeView.Width;
            listboxOfPlanetarySystems.Height = planetarySystemTreeView.Height;
            listboxOfPlanetarySystems.Top = planetarySystemTreeView.Top;
            listboxOfPlanetarySystems.Left = planetarySystemTreeView.Left;
        }

        private void LoadCreateNewGroup()
        {
            createNewTypeBox.Items.Add("Planetary system");
            createNewTypeBox.Items.Add("Planet");
            createNewTypeBox.Items.Add("Moon");
            createNewTypeBox.Items.Add("Asteroid");
            createNewTypeBox.Items.Add("Artificial object");
            createNewTypeBox.SelectedIndex = 0;
            //createNewNameBox.GotFocus += new System.EventHandler(CreateNewNameBoxClearText);
            //createNewNameBox.LostFocus += new System.EventHandler(CreateNewNameBoxAddDefaultText);
        }

        private void LoadPlanetarySystemNamesToList()
        {
            List<string> planetarySystemNames = Database.GetAllPlanetarySystemNames();
            foreach (var name in planetarySystemNames)
            {
                listboxOfPlanetarySystems.Items.Add(name);
            }
        }

        #region LoadInfoRegion
        private void LoadPlanetarySystemToTree(PlanetarySystem planetarySystem)
        {
            listboxOfPlanetarySystems.Visible = false;
            planetarySystemTreeView.Nodes.Clear();
            planetarySystemTreeView.Nodes.Add(planetarySystem.Name);
            var planetarySystemNode = planetarySystemTreeView.Nodes[0];
            if (planetarySystem.Star != null)
            {
                planetarySystemNode.Nodes.Add($"Star: {planetarySystem.Star.Name}");
            }
            else
            {
                planetarySystemNode.Nodes.Add($"No star assigned");
            }

            var starNode = planetarySystemNode.Nodes[0];

            planetarySystemNode.Nodes.Add("Planets:");
            var planetsNode = planetarySystemNode.Nodes[1];
            var planets = planetarySystem.Planets.ToList();
            for (int i = 0; i < planets.Count; i++)
            {
                planetsNode.Nodes.Add(planets[i].Name);
                var planetNode = planetsNode.Nodes[i];
                planetNode.Nodes.Add("Moons:");
                var moonsNode = planetNode.Nodes[0];
                foreach (var m in planets[i].Moons)
                {
                    moonsNode.Nodes.Add(m.Name);
                }
            }

            planetarySystemNode.Nodes.Add("Asteroids:");
            var asteroidsNode = planetarySystemNode.Nodes[2];
            foreach (var asteroid in planetarySystem.Asteroids)
            {
                asteroidsNode.Nodes.Add(asteroid.Name);
            }

            planetarySystemNode.Nodes.Add("Artificial objects:");
            var artificialObjectsNode = planetarySystemNode.Nodes[3];
            foreach (var artObj in planetarySystem.ArtificialObjects)
            {
                artificialObjectsNode.Nodes.Add(artObj.Name);
            }
        }

        private void LoadBodyInfo(AstronomicalBody body)
        {
            LoadGeneralInfo(body);
            LoadPositionInfo(body);
            LoadVelocityInfo(body);
        }

        private void LoadGeneralInfo(AstronomicalBody body)
        {
            // Name
            nameBox.Text = body.Name;

            // Type

            if (body is Star)
            {
                typeBox.Text = "Star";
            }
            else if (body is Planet)
            {
                typeBox.Text = "Planet";
            }
            else if (body is Moon)
            {
                typeBox.Text = "Moon";
            }
            else if (body is Asteroid)
            {
                typeBox.Text = "Asteroid";
            }
            else if (body is ArtificialObject)
            {
                typeBox.Text = "Artificial object";
            }

            // Mass
            double number = 0;
            int e = 0;
            MathUtilities.ParseToExpNotation(body.Mass, out number, out e);
            massBox.Text = number.ToString();
            massEBox.Text = e.ToString();

            // Radius
            MathUtilities.ParseToExpNotation(body.Radius, out number, out e);
            radiusBox.Text = number.ToString();
            radiusEBox.Text = e.ToString();

            // Density
            MathUtilities.ParseToExpNotation(body.Density, out number, out e);
            densityBox.Text = number.ToString();
            densityEBox.Text = e.ToString();
        }

        private void LoadPositionInfo(AstronomicalBody body)
        {
            double number = 0;
            int e = 0;
            MathUtilities.ParseToExpNotation(body.Center.X, out number, out e);
            posXBox.Text = number.ToString();
            posXEBox.Text = e.ToString();
            MathUtilities.ParseToExpNotation(body.Center.Y, out number, out e);
            posYBox.Text = number.ToString();
            posYEBox.Text = e.ToString();
            MathUtilities.ParseToExpNotation(body.Center.Z, out number, out e);
            posZBox.Text = number.ToString();
            posZEBox.Text = e.ToString();
        }

        private void LoadVelocityInfo(AstronomicalBody body)
        {
            velXBox.Text = body.Velocity.X.ToString();
            velYBox.Text = body.Velocity.Y.ToString();
            velZBox.Text = body.Velocity.Z.ToString();
            velLengthBox.Text = body.Velocity.Length.ToString();
            velThetaBox.Text = body.Velocity.Theta.ToString();
            velPhiBox.Text = body.Velocity.Phi.ToString();
        }
        #endregion

        public void ClearPropertiesControls()
        {
            nameBox.Text = "";
            typeBox.Text = "";
            massBox.Text = "";
            massEBox.Text = "";
            radiusBox.Text = "";
            radiusEBox.Text = "";
            densityBox.Text = "";
            densityEBox.Text = "";

            posXBox.Text = "";
            posXEBox.Text = "";
            posYBox.Text = "";
            posYEBox.Text = "";
            posZBox.Text = "";
            posZEBox.Text = "";

            velXBox.Text = "";
            velYBox.Text = "";
            velZBox.Text = "";
            velLengthBox.Text = "";
            velThetaBox.Text = "";
            velPhiBox.Text = "";
        }

        #region NumericalInputFiltering
        private void NumberForce(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8 || e.KeyChar == 45 || e.KeyChar == 46))
            {
                e.Handled = true;
            }
        }

        private void IntegerForce(object sender, KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 48 && e.KeyChar <= 57) || e.KeyChar == 8 || e.KeyChar == 45))
            {
                e.Handled = true;
            }
        }
        #endregion

        private void planetarySystemTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text != currentPlanetarySystem.Name &&
                e.Node.Text != "No star assigned" &&
                e.Node.Text != "Planets:" &&
                e.Node.Text != "Moons:" &&
                e.Node.Text != "Asteroids:" &&
                e.Node.Text != "Artificial objects:")
            {
                var name = e.Node.Text;
                if (name.IndexOf("Star: ") >= 0)
                {
                    name = name.Remove(0, 6);
                }
                var allBodies = currentPlanetarySystem.GetAllBodies();
                var selectedBodyQuery = from b in allBodies
                                        where b.Name == name
                                        select b;
                selectedBody = selectedBodyQuery.FirstOrDefault();
                LoadBodyInfo(selectedBody);
            }
        }

        private void listboxOfPlanetarySystems_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentPlanetarySystem = Database.LoadPlanetarySystem(listboxOfPlanetarySystems.SelectedItem.ToString());
            LoadPlanetarySystemToTree(currentPlanetarySystem);
            ClearPropertiesControls();
            listboxOfPlanetarySystems.Visible = false;
            listboxOfPlanetarySystems.Items.Clear();
            planetarySystemTreeView.Visible = true;
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            planetarySystemTreeView.Visible = false;
            listboxOfPlanetarySystems.Visible = true;
            ClearPropertiesControls();
            ClearCreateNewSection();
            LoadPlanetarySystemNamesToList();
        }

        private void saveBodyChangesButton_Click(object sender, EventArgs e)
        {
            selectedBody.Name = nameBox.Text;
            selectedBody.Mass = MathUtilities.ParseFromExpNotation(double.Parse(massBox.Text), int.Parse(massEBox.Text));
            selectedBody.Radius = MathUtilities.ParseFromExpNotation(double.Parse(radiusBox.Text), int.Parse(radiusEBox.Text));

            selectedBody.Center.X = MathUtilities.ParseFromExpNotation(double.Parse(posXBox.Text), int.Parse(posXEBox.Text));
            selectedBody.Center.Y = MathUtilities.ParseFromExpNotation(double.Parse(posYBox.Text), int.Parse(posYEBox.Text));
            selectedBody.Center.Z = MathUtilities.ParseFromExpNotation(double.Parse(posZBox.Text), int.Parse(posZEBox.Text));

            selectedBody.Velocity.X = double.Parse(velXBox.Text);
            selectedBody.Velocity.Y = double.Parse(velYBox.Text);
            selectedBody.Velocity.Z = double.Parse(velZBox.Text);
        }

        private void velXBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                selectedBody.Velocity.X = double.Parse(velXBox.Text);
                LoadVelocityInfo(selectedBody);
            }
            catch (Exception)
            {
            }
        }

        private void velYBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                selectedBody.Velocity.Y = double.Parse(velYBox.Text);
                LoadVelocityInfo(selectedBody);
            }
            catch (Exception)
            {
            }
        }

        private void velZBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                selectedBody.Velocity.Z = double.Parse(velZBox.Text);
                LoadVelocityInfo(selectedBody);
            }
            catch (Exception)
            {
            }
        }

        private void velLengthBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                selectedBody.Velocity.Length = double.Parse(velLengthBox.Text);
                LoadVelocityInfo(selectedBody);
            }
            catch (Exception)
            {
            }

        }

        private void velThetaBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                selectedBody.Velocity.Theta = double.Parse(velThetaBox.Text);
                LoadVelocityInfo(selectedBody);
            }
            catch (Exception)
            {
            }

        }

        private void velPhiBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                selectedBody.Velocity.Phi = double.Parse(velPhiBox.Text);
                LoadVelocityInfo(selectedBody);
            }
            catch (Exception)
            {
            }

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Database.SavePlanetarySystem(currentPlanetarySystem);
        }

        private void createNewTypeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (createNewTypeBox.SelectedIndex)
            {
                case 0:
                    createNewPrimeSelectionBox.Items.Clear();
                    createNewPrimeSelectionBox.Enabled = false;
                    createNewButton.Enabled = true;
                    createNewMassBox.Enabled = false;
                    createNewMassEBox.Enabled = false;
                    createNewRadiusBox.Enabled = false;
                    createNewRadiusEBox.Enabled = false;
                    orbitGroupBox.Enabled = false;
                    break;
                case 1:
                    createNewPrimeSelectionBox.Items.Clear();
                    if (currentPlanetarySystem != null)
                    {
                        createNewPrimeSelectionBox.Items.Add(currentPlanetarySystem.Star.Name);
                        createNewPrimeSelectionBox.SelectedIndex = 0;
                        createNewPrimeSelectionBox.Enabled = false;
                        createNewButton.Enabled = true;
                        createNewMassBox.Enabled = true;
                        createNewMassEBox.Enabled = true;
                        createNewRadiusBox.Enabled = true;
                        createNewRadiusEBox.Enabled = true;
                        orbitGroupBox.Enabled = true;
                    }
                    break;
                case 2:
                    createNewPrimeSelectionBox.Items.Clear();
                    if (currentPlanetarySystem != null)
                    {
                        if (currentPlanetarySystem.Planets.Count > 0)
                        {
                            createNewPrimeSelectionBox.Items.Clear();
                            foreach (var p in currentPlanetarySystem.Planets)
                            {
                                createNewPrimeSelectionBox.Items.Add(p.Name);
                            }

                            createNewPrimeSelectionBox.SelectedIndex = 0;
                            createNewPrimeSelectionBox.Enabled = true;
                            createNewButton.Enabled = true;
                            createNewMassBox.Enabled = true;
                            createNewMassEBox.Enabled = true;
                            createNewRadiusBox.Enabled = true;
                            createNewRadiusEBox.Enabled = true;
                            orbitGroupBox.Enabled = true;
                        }
                        else
                        {
                            createNewPrimeSelectionBox.Enabled = false;
                            createNewButton.Enabled = false;
                            createNewMassBox.Enabled = false;
                            createNewMassEBox.Enabled = false;
                            createNewRadiusBox.Enabled = false;
                            createNewRadiusEBox.Enabled = false;
                            orbitGroupBox.Enabled = false;
                        }
                    }
                    break;
                case 3:
                    createNewPrimeSelectionBox.Items.Clear();
                    if (currentPlanetarySystem != null)
                    {
                        createNewPrimeSelectionBox.Enabled = false;
                        createNewButton.Enabled = true;
                        createNewMassBox.Enabled = true;
                        createNewMassEBox.Enabled = true;
                        createNewRadiusBox.Enabled = true;
                        createNewRadiusEBox.Enabled = true;
                        orbitGroupBox.Enabled = true;
                    }
                    break;
                case 4:
                    createNewPrimeSelectionBox.Items.Clear();
                    if (currentPlanetarySystem != null)
                    {
                        createNewPrimeSelectionBox.Enabled = false;
                        createNewButton.Enabled = true;
                        createNewMassBox.Enabled = true;
                        createNewMassEBox.Enabled = true;
                        createNewRadiusBox.Enabled = true;
                        createNewRadiusEBox.Enabled = true;
                        orbitGroupBox.Enabled = true;
                    }
                    break;
            }
        }

        private void ClearCreateNewSection()
        {
            createNewPrimeSelectionBox.Items.Clear();
        }

        private void createNewButton_Click(object sender, EventArgs e)
        {
            ClearCreateNewSection();
            try
            {

                switch (createNewTypeBox.SelectedIndex)
                {
                    case 0:
                        currentPlanetarySystem = new PlanetarySystem(createNewNameBox.Text);
                        Star star = new Star(
                            new PlanetSystem.Models.Utilities.Point(0, 0, 0),
                            1,
                            1,
                            createNewNameBox.Text + "\' star");
                        currentPlanetarySystem.SetStar(star);
                        break;
                    case 1:
                        Planet planet = new Planet(
                            new PlanetSystem.Models.Utilities.Point(0, 0, 0),
                            MathUtilities.ParseFromExpNotation(double.Parse(createNewMassBox.Text), int.Parse(createNewMassEBox.Text)),
                            MathUtilities.ParseFromExpNotation(double.Parse(createNewRadiusBox.Text), int.Parse(createNewRadiusEBox.Text)),
                            createNewNameBox.Text);

                        if (orbitByRadiusRadio.Checked)
                        {
                            currentPlanetarySystem.AddPlanetByOrbitalRadius(
                                planet,
                                MathUtilities.ParseFromExpNotation(double.Parse(orbitalParameterBox.Text), int.Parse(orbitalParameterEBox.Text)));
                        }
                        else
                        {
                            currentPlanetarySystem.AddPlanetByOrbitalSpeed(
                                planet,
                                MathUtilities.ParseFromExpNotation(double.Parse(orbitalParameterBox.Text), int.Parse(orbitalParameterEBox.Text)));
                        }
                        break;
                    case 2:
                        var primeQuery = from p in currentPlanetarySystem.Planets
                                         where p.Name == (string)createNewPrimeSelectionBox.SelectedItem
                                         select p;
                        var prime = primeQuery.FirstOrDefault();
                        Moon moon = new Moon(
                            new PlanetSystem.Models.Utilities.Point(0, 0, 0),
                            MathUtilities.ParseFromExpNotation(double.Parse(createNewMassBox.Text), int.Parse(createNewMassEBox.Text)),
                            MathUtilities.ParseFromExpNotation(double.Parse(createNewRadiusBox.Text), int.Parse(createNewRadiusEBox.Text)),
                            createNewNameBox.Text);

                        if (orbitByRadiusRadio.Checked)
                        {
                            currentPlanetarySystem.AddMoonByOrbitalRadius(
                                moon,
                                prime,
                                MathUtilities.ParseFromExpNotation(double.Parse(orbitalParameterBox.Text), int.Parse(orbitalParameterEBox.Text)));
                        }
                        else
                        {
                            currentPlanetarySystem.AddMoonByOrbitalSpeed(
                                moon,
                                prime,
                                MathUtilities.ParseFromExpNotation(double.Parse(orbitalParameterBox.Text), int.Parse(orbitalParameterEBox.Text)));
                        }
                        break;
                    case 3:
                        Asteroid asteroid = new Asteroid(
                            new PlanetSystem.Models.Utilities.Point(0, 0, 0),
                            MathUtilities.ParseFromExpNotation(double.Parse(createNewMassBox.Text), int.Parse(createNewMassEBox.Text)),
                            MathUtilities.ParseFromExpNotation(double.Parse(createNewRadiusBox.Text), int.Parse(createNewRadiusEBox.Text)),
                            createNewNameBox.Text);
                        currentPlanetarySystem.AddAsteroid(asteroid);
                        break;
                    case 4:
                        ArtificialObject artObj = new ArtificialObject(new PlanetSystem.Models.Utilities.Point(0, 0, 0),
                            MathUtilities.ParseFromExpNotation(double.Parse(createNewMassBox.Text), int.Parse(createNewMassEBox.Text)),
                            MathUtilities.ParseFromExpNotation(double.Parse(createNewRadiusBox.Text), int.Parse(createNewRadiusEBox.Text)),
                            createNewNameBox.Text);
                        currentPlanetarySystem.AddArtificialObject(artObj);
                        break;
                }
                LoadPlanetarySystemToTree(currentPlanetarySystem);
            }
            catch (Exception exc)
            {
            }
        }
    }
}
