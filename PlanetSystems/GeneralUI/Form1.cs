using PlanetSystem.Data;
using PlanetSystem.Models.Bodies;
using PlanetSystem.Models.Utilities;
using ReportsGenerators;
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
        PlanetarySystem backupPlanetarySystem;
        PlanetarySystem currentPlanetarySystem;
        AstronomicalBody selectedBody;
        Plane planeOfGraph = Plane.XY;
        double scaleFactor = 0;
        Bitmap canvasBitmap;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCreateNewGroup();
            canvasPanel.BackColor = Color.White;
            canvasBitmap = new Bitmap(canvasPanel.Width, canvasPanel.Height);
            ClearCanvas();


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

            planeBox.Items.Add("XY");
            planeBox.Items.Add("XZ");
            planeBox.Items.Add("YZ");
            planeBox.SelectedIndex = 0;
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
            if (currentPlanetarySystem != null)
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

                planetarySystemTreeView.Nodes[0].Expand();
            }
            else
            {
                if (planetarySystemTreeView.Nodes.Count > 0)
                {
                    planetarySystemTreeView.Nodes.Remove(planetarySystemTreeView.Nodes[0]);
                }
            }
        }

        private void LoadBodyInfo(AstronomicalBody body)
        {
            try
            {
                LoadGeneralInfo(body);
                LoadPositionInfo(body);
                LoadVelocityInfo(body);
            }
            catch (Exception exc)
            {
            }

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
            SetScale();
            Backup();
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            try
            {
                planetarySystemTreeView.Visible = false;
                listboxOfPlanetarySystems.Visible = true;
                ClearPropertiesControls();
                ClearCreateNewSection();
                LoadPlanetarySystemNamesToList();
            }
            catch (Exception exc)
            {
            }
        }

        private void saveBodyChangesButton_Click(object sender, EventArgs eventArgs)
        {
            try
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

                Refresh();
                Backup();
            }
            catch (Exception exc)
            {
            }
 
        }

        #region Auto Update Velocity
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
        #endregion

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                Database.SavePlanetarySystem(currentPlanetarySystem);
                Backup();
            }
            catch (Exception exc)
            {
            }
  
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

        private void Backup()
        {
            backupPlanetarySystem = currentPlanetarySystem.Clone();
        }

        private void Restore()
        {
            currentPlanetarySystem = backupPlanetarySystem.Clone();
        }

        private void createNewButton_Click(object sender, EventArgs e)
        {
            try
            {
                ClearCreateNewSection();
                double coveredAngle = 0;
                try
                {
                    coveredAngle = double.Parse(createNewCoveredAngleBox.Text);
                }
                catch (Exception exc)
                { }

                double mass = 0;
                double radius = 0;
                double orbitalParameter = 0;
                if (createNewTypeBox.SelectedIndex != 0)
                {
                    mass = MathUtilities.ParseFromExpNotation(double.Parse(createNewMassBox.Text), int.Parse(createNewMassEBox.Text));
                    radius = MathUtilities.ParseFromExpNotation(double.Parse(createNewRadiusBox.Text), int.Parse(createNewRadiusEBox.Text));
                    orbitalParameter = MathUtilities.ParseFromExpNotation(double.Parse(orbitalParameterBox.Text), int.Parse(orbitalParameterEBox.Text));
                }

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
                            mass,
                            radius,
                            createNewNameBox.Text);

                        if (orbitByRadiusRadio.Checked)
                        {
                            currentPlanetarySystem.AddPlanetByOrbitalRadius(
                                planet,
                                orbitalParameter,
                                coveredAngle);
                        }
                        else
                        {
                            currentPlanetarySystem.AddPlanetByOrbitalSpeed(
                                planet,
                                orbitalParameter,
                                coveredAngle);
                        }
                        break;
                    case 2:
                        string planetName = createNewPrimeSelectionBox.Text;
                        var primeQuery = from p in currentPlanetarySystem.Planets
                                         where p.Name == planetName
                                         select p;
                        var prime = primeQuery.FirstOrDefault();
                        Moon moon = new Moon(
                            new PlanetSystem.Models.Utilities.Point(0, 0, 0),
                            mass,
                            radius,
                            createNewNameBox.Text);

                        if (orbitByRadiusRadio.Checked)
                        {
                            currentPlanetarySystem.AddMoonByOrbitalRadius(
                                moon,
                                prime,
                                orbitalParameter,
                                coveredAngle);
                        }
                        else
                        {
                            currentPlanetarySystem.AddMoonByOrbitalSpeed(
                                moon,
                                prime,
                                orbitalParameter,
                                coveredAngle);
                        }
                        break;
                    case 3:
                        Asteroid asteroid = new Asteroid(
                            new PlanetSystem.Models.Utilities.Point(0, 0, 0),
                            mass,
                            radius,
                            createNewNameBox.Text);
                        currentPlanetarySystem.AddAsteroid(asteroid);
                        break;
                    case 4:
                        ArtificialObject artObj = new ArtificialObject(new PlanetSystem.Models.Utilities.Point(0, 0, 0),
                            mass,
                            radius,
                            createNewNameBox.Text);
                        currentPlanetarySystem.AddArtificialObject(artObj);
                        break;
                }
                LoadPlanetarySystemToTree(currentPlanetarySystem);
                Backup();
            }
            catch (Exception exc)
            {
            }
        }

        private void simulateButton_Click(object sender, EventArgs e)
        {
            try
            {
                var stepCount = int.Parse(stepsCountBox.Text);
                var stepDuration = double.Parse(stepDurationBox.Text);
                List<List<PlanetSystem.Models.Utilities.Point>> positions = new List<List<PlanetSystem.Models.Utilities.Point>>();
                var startingPositions = currentPlanetarySystem.GetAllPositions();
                startingPositions.ForEach(p => p = new PlanetSystem.Models.Utilities.Point(p));
                positions.Add(startingPositions);

                double greatestDistance;
                PlanetSystem.Models.Utilities.Point centerPointCandidate = new PlanetSystem.Models.Utilities.Point(0, 0, 0);
                greatestDistance = Physics.GetGreatestDistance(currentPlanetarySystem.GetAllBodies(), out centerPointCandidate);

                PlanetSystem.Models.Utilities.Point centerPoint = new PlanetSystem.Models.Utilities.Point(0, 0, 0);

                for (int i = 0; i < stepCount; i++)
                {
                    currentPlanetarySystem.AdvanceTime(currentPlanetarySystem.GetAllBodies(), stepDuration);
                    var currentPositions = currentPlanetarySystem.GetAllPositions();
                    currentPositions.ForEach(p => p = new PlanetSystem.Models.Utilities.Point(p));
                    positions.Add(currentPositions);
                    var currentDistance = Physics.GetGreatestDistance(currentPlanetarySystem.GetAllBodies(), out centerPointCandidate);
                    if (currentDistance > greatestDistance)
                    {
                        greatestDistance = currentDistance;
                        centerPoint = new PlanetSystem.Models.Utilities.Point(centerPointCandidate);
                    }
                }

                var curves = GetCurvesFromPositions(positions);
                curves = ScalePositions(curves, greatestDistance, canvasPanel.Width);
                var drawableCurves = ConvertToDrawableCurves(curves, planeOfGraph,
                    new System.Drawing.Point(canvasPanel.Width / 2, canvasPanel.Height / 2));
                DrawCurves(drawableCurves);
                Refresh();
            }
            catch (Exception exc)
            {
            }
        }

        private void SetScale()
        {
            PlanetSystem.Models.Utilities.Point dummy = new PlanetSystem.Models.Utilities.Point(0,0,0);
            var bodies = currentPlanetarySystem.GetAllBodies();
            double greatestDistance = Physics.GetGreatestDistance(bodies, out dummy);
            scaleFactor = canvasPanel.Width / (greatestDistance * 2.5);
        }

        private List<List<PlanetSystem.Models.Utilities.Point>> GetCurvesFromPositions(List<List<PlanetSystem.Models.Utilities.Point>> positions)
        {
            List<List<PlanetSystem.Models.Utilities.Point>> curves = new List<List<PlanetSystem.Models.Utilities.Point>>();
            for (int i = 0; i < positions[i].Count; i++)
            {
                curves.Add(new List<PlanetSystem.Models.Utilities.Point>());
            }

            for (int i = 0; i < positions.Count; i++)
            {
                for (int j = 0; j < positions[i].Count; j++)
                {
                    curves[j].Add(new PlanetSystem.Models.Utilities.Point(positions[i][j]));
                }
            }

            return curves;
        }

        private List<List<System.Drawing.Point>> ConvertToDrawableCurves(
            List<List<PlanetSystem.Models.Utilities.Point>> curves,
            Plane plane, System.Drawing.Point canvasCenter)
        {
            List<List<System.Drawing.Point>> drawable = new List<List<System.Drawing.Point>>();
            for (int i = 0; i < curves.Count; i++)
            {
                drawable.Add(new List<System.Drawing.Point>());
            }
            for (int i = 0; i < curves.Count; i++)
            {
                for (int j = 0; j < curves[0].Count; j++)
                {
                    switch (plane)
                    {
                        case Plane.XY:
                            drawable[i].Add(new System.Drawing.Point(
                                (int)curves[i][j].X + canvasCenter.X,
                                (int)curves[i][j].Y + canvasCenter.Y));
                            break;
                        case Plane.XZ:
                            drawable[i].Add(new System.Drawing.Point(
                                (int)curves[i][j].X + canvasCenter.X,
                                (int)curves[i][j].Z + canvasCenter.Y));
                            break;
                        case Plane.YZ:
                            drawable[i].Add(new System.Drawing.Point(
                                (int)curves[i][j].Y + canvasCenter.X,
                                (int)curves[i][j].Z + canvasCenter.Y));
                            break;
                        default:
                            break;
                    }
                }
            }
            return drawable;
        }

        private void CenterPositions(
            ref List<List<PlanetSystem.Models.Utilities.Point>> positions,
            PlanetSystem.Models.Utilities.Point centerPoint)
        {          
            positions.ForEach(l => { PlanetSystem.Models.Utilities.Point.OffsetBy(l, centerPoint.GetOpposite()); });
        }

        private List<List<PlanetSystem.Models.Utilities.Point>> ScalePositions(List<List<PlanetSystem.Models.Utilities.Point>> positions,
            double greatestDistance,
            double width)
        {
            positions.ForEach(l =>
            {
                l.ForEach(p =>
                {
                    p.X = p.X * scaleFactor;
                    p.Y = p.Y * scaleFactor;
                    p.Z = p.Z * scaleFactor;
                });
            });
            return positions;
        }

        private void DrawCurves(List<List<System.Drawing.Point>> curves)
        {            
            Graphics panelGraphics = canvasPanel.CreateGraphics();
            Graphics bmpGraphics = Graphics.FromImage(canvasBitmap);
            Pen pen = new Pen(Color.Black, 3);

            curves.ForEach(c =>
            {
                panelGraphics.DrawCurve(pen, c.ToArray());
                bmpGraphics.DrawCurve(pen, c.ToArray());
            });

            pen.Dispose();
            panelGraphics.Dispose();
            bmpGraphics.Dispose();
        }

        private void Refresh()
        {
            if (selectedBody != null)
            {
                LoadBodyInfo(selectedBody);
            }

            LoadPlanetarySystemToTree(currentPlanetarySystem);
        }

        private void planeBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (planeBox.SelectedIndex)
            {
                case 0:
                    if (planeOfGraph != Plane.XY)
                    {
                        ClearCanvas();
                    }

                    planeOfGraph = Plane.XY; break;

                case 1:
                    if (planeOfGraph != Plane.XZ)
                    {
                        ClearCanvas();
                    }
                    planeOfGraph = Plane.XZ; break;
                case 2:
                    if (planeOfGraph != Plane.YZ)
                    {
                        ClearCanvas();
                    }
                    planeOfGraph = Plane.YZ; break;
                default:
                    break;
            }
        }

        private void ClearCanvas()
        {
            using (Graphics g = canvasPanel.CreateGraphics())
            {
                g.Clear(Color.White);
            }
            using (Graphics bmpGraph = Graphics.FromImage(canvasBitmap))
            {
                bmpGraph.Clear(Color.White);
            }
        }

        private void clearGraphButton_Click(object sender, EventArgs e)
        {
            ClearCanvas();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (currentPlanetarySystem != null && planetarySystemTreeView.Nodes[0].IsSelected)
            {
                Database.DeletePlanetarySystem(currentPlanetarySystem.Name);
            }
            else
            {
                if (selectedBody != null)
                {
                    if (selectedBody is Planet)
                    {
                        currentPlanetarySystem.RemovePlanet(selectedBody.Name);
                    }
                    else if (selectedBody is Moon)
                    {
                        string planetName = planetarySystemTreeView.SelectedNode.Parent.Parent.Text;
                        var planetQuery = from p in currentPlanetarySystem.Planets
                                          where p.Name == planetName
                                          select p;
                        Planet planet = planetQuery.FirstOrDefault();
                        if (planet != null)
                        {
                            planet.RemoveMoon(selectedBody.Name);
                        }
                    }
                    else if (selectedBody is Asteroid)
                    {
                        currentPlanetarySystem.RemoveAsteroid(selectedBody.Name);
                    }
                    else if (selectedBody is ArtificialObject)
                    {
                        currentPlanetarySystem.RemoveArtificialObject(selectedBody.Name);
                    }
                }

                Refresh();
            }
        }

        private void revertButton_Click(object sender, EventArgs e)
        {
            Restore();
            Refresh();
        }

        private void setOrbitButon_Click(object sender, EventArgs e)
        {
            try
            {
                double coveredAngle = 0;
                try
                {
                    coveredAngle = double.Parse(setOrbitCoveredAngleBox.Text);
                }
                catch (Exception exc)
                { }
                double orbitParameter = MathUtilities.ParseFromExpNotation(
                    double.Parse(setOrbitParameterBox.Text),
                    int.Parse(setOrbitParameterEBox.Text));
                if (selectedBody is Planet)
                {
                    Planet planet = (Planet)selectedBody;
                    if (setOrbitRadiusRadio.Checked)
                    {
                        Vector startingVelocity = new Vector(planet.Velocity);
                        PlanetSystem.Models.Utilities.Point startingPoint = new PlanetSystem.Models.Utilities.Point(planet.Center);
                        if (setOrbitRadiusRadio.Checked)
                        {
                            Physics.EnterOrbitByGivenRadius(ref planet, currentPlanetarySystem.Star, orbitParameter, coveredAngle);
                        }
                        else
                        {
                            Physics.EnterOrbitByGivenSpeed(ref planet, currentPlanetarySystem.Star, orbitParameter, coveredAngle);
                        }
                        Vector deltaVelocity = planet.Velocity - startingVelocity;
                        PlanetSystem.Models.Utilities.Point deltaPoint = planet.Center - startingPoint;
                        planet.Moons.ToList().ForEach(m =>
                        {
                            Physics.AddVelocityToBody(ref m, deltaVelocity);
                            m.Center = new PlanetSystem.Models.Utilities.Point(
                                m.Center.X + deltaPoint.X,
                                m.Center.Y + deltaPoint.Y,
                                m.Center.X + deltaPoint.Z
                                );
                        });
                    }
                    else
                    {
                        Vector startingVelocity = new Vector(planet.Velocity);
                        PlanetSystem.Models.Utilities.Point startingPoint = new PlanetSystem.Models.Utilities.Point(planet.Center);
                        if (setOrbitRadiusRadio.Checked)
                        {
                            Physics.EnterOrbitByGivenSpeed(ref planet, currentPlanetarySystem.Star, orbitParameter, coveredAngle);
                        }
                        else
                        {
                            Physics.EnterOrbitByGivenSpeed(ref planet, currentPlanetarySystem.Star, orbitParameter, coveredAngle);
                        }
                        Vector deltaVelocity = planet.Velocity - startingVelocity;
                        PlanetSystem.Models.Utilities.Point deltaPoint = planet.Center - startingPoint;
                        planet.Moons.ToList().ForEach(m =>
                        {
                            Physics.AddVelocityToBody(ref m, deltaVelocity);
                            m.Center = new PlanetSystem.Models.Utilities.Point(
                                m.Center.X + deltaPoint.X,
                                m.Center.Y + deltaPoint.Y,
                                m.Center.X + deltaPoint.Z
                                );
                        });
                    }
                }
                else if (selectedBody is Moon)
                {
                    Moon moon = (Moon)selectedBody;
                    if (setOrbitRadiusRadio.Checked)
                    {
                        Physics.EnterOrbitByGivenRadius(ref moon, moon.Planet, orbitParameter, coveredAngle);
                    }
                    else
                    {
                        Physics.EnterOrbitByGivenSpeed(ref moon, moon.Planet, orbitParameter, coveredAngle);
                    }
                }
                else if (selectedBody is Asteroid)
                {
                    Asteroid asteroid = (Asteroid)selectedBody;
                    var bodies = currentPlanetarySystem.GetAllBodies();
                    var primaryQuery = from p in bodies
                                       where p.Name == setOrbitPrimeBox.SelectedText
                                       select p;

                    AstronomicalBody primary = primaryQuery.FirstOrDefault();
                    if (setOrbitRadiusRadio.Checked)
                    {
                        Physics.EnterOrbitByGivenRadius(ref asteroid, primary, orbitParameter, coveredAngle);
                    }
                    else
                    {
                        Physics.EnterOrbitByGivenSpeed(ref asteroid, primary, orbitParameter, coveredAngle);
                    }
                }
                else if (selectedBody is ArtificialObject)
                {
                    ArtificialObject artificialObject = (ArtificialObject)selectedBody;
                    var bodies = currentPlanetarySystem.GetAllBodies();
                    var primaryQuery = from p in bodies
                                       where p.Name == setOrbitPrimeBox.SelectedText
                                       select p;

                    AstronomicalBody primary = primaryQuery.FirstOrDefault();
                    if (setOrbitRadiusRadio.Checked)
                    {
                        Physics.EnterOrbitByGivenRadius(ref artificialObject, primary, orbitParameter, coveredAngle);
                    }
                    else
                    {
                        Physics.EnterOrbitByGivenSpeed(ref artificialObject, primary, orbitParameter, coveredAngle);
                    }
                }
                else
                {
                    var bodies = currentPlanetarySystem.GetAllBodies();
                    var primaryQuery = from p in bodies
                                       where p.Name == setOrbitPrimeBox.SelectedText
                                       select p;

                    AstronomicalBody primary = primaryQuery.FirstOrDefault();
                    if (setOrbitRadiusRadio.Checked)
                    {
                        Physics.EnterOrbitByGivenRadius(ref selectedBody, primary, orbitParameter, coveredAngle);
                    }
                    else
                    {
                        Physics.EnterOrbitByGivenSpeed(ref selectedBody, primary, orbitParameter, coveredAngle);
                    }
                }
                Refresh();
            }
            catch (Exception exc)
            {
            }
        }

        private void rescaleButton_Click(object sender, EventArgs e)
        {
            SetScale();
        }

        private void orbitByRadiusRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (orbitByRadiusRadio.Checked)
            {
                orbitalParameterLabel.Text = "Radius";
            }
            else
            {
                orbitalParameterLabel.Text = "Speed";
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            Refresh();
        }

        private void generateReport_Click(object sender, EventArgs e)
        {
            GeneralUIReportGenerator.GenerateReport(
                currentPlanetarySystem.Name,
                backupPlanetarySystem.GetAllBodies(),
                currentPlanetarySystem.GetAllBodies(),
                canvasBitmap);
        }
    }
}
