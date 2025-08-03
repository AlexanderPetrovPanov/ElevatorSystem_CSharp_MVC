using System;
using System.Windows.Forms;

public class ElevatorView : Form
{
    private Label currentFloorLabel;
    private Label statusLabel;
    private Button[] floorButtons;
    private Button[] upButtons;
    private Button[] downButtons;
    private TextBox weightField;
    private ElevatorController controller;

    public ElevatorView(ElevatorController controller)
    {
        this.controller = controller;
        Initialize();
    }

    private void Initialize()
    {
        this.Text = "Elevator Control System";
        this.Size = new System.Drawing.Size(600, 500);
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;

        // Current status panel
        Panel statusPanel = new Panel();
        statusPanel.Dock = DockStyle.Top;
        statusPanel.Height = 80;
        
        currentFloorLabel = new Label();
        currentFloorLabel.Text = "Current Floor: 0";
        currentFloorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        currentFloorLabel.Font = new System.Drawing.Font("Arial", 24, System.Drawing.FontStyle.Bold);
        currentFloorLabel.Dock = DockStyle.Top;
        statusPanel.Controls.Add(currentFloorLabel);

        statusLabel = new Label();
        statusLabel.Text = "Status: Stationary";
        statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        statusLabel.Font = new System.Drawing.Font("Arial", 18);
        statusLabel.Dock = DockStyle.Top;
        statusPanel.Controls.Add(statusLabel);

        this.Controls.Add(statusPanel);

        // Main panel with elevator controls
        Panel mainPanel = new Panel();
        mainPanel.Dock = DockStyle.Fill;

        // Elevator buttons panel
        GroupBox elevatorGroup = new GroupBox();
        elevatorGroup.Text = "Elevator Buttons";
        elevatorGroup.Dock = DockStyle.Left;
        elevatorGroup.Width = this.Width / 2;
        
        Panel elevatorPanel = new Panel();
        elevatorPanel.Dock = DockStyle.Fill;
        elevatorPanel.AutoScroll = true;
        
        floorButtons = new Button[9];
        
        for (int i = 7; i >= -1; i--)
        {
            int floor = i;
            floorButtons[i + 1] = new Button();
            floorButtons[i + 1].Text = $"Floor {i}";
            floorButtons[i + 1].Dock = DockStyle.Top;
            floorButtons[i + 1].Height = 40;
            floorButtons[i + 1].Click += (sender, e) => controller.HandleFloorButtonPress(floor);
            elevatorPanel.Controls.Add(floorButtons[i + 1]);
        }
        
        elevatorGroup.Controls.Add(elevatorPanel);
        mainPanel.Controls.Add(elevatorGroup);

        // Floor call buttons panel
        GroupBox callGroup = new GroupBox();
        callGroup.Text = "Call Buttons";
        callGroup.Dock = DockStyle.Fill;
        
        TableLayoutPanel callPanel = new TableLayoutPanel();
        callPanel.Dock = DockStyle.Fill;
        callPanel.AutoScroll = true;
        callPanel.ColumnCount = 2;
        callPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        callPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        
        upButtons = new Button[8];
        downButtons = new Button[8];

        for (int i = 7; i >= -1; i--)
        {
            int floor = i;
            if (i == 7)
            {
                // Only down button for top floor
                downButtons[7] = new Button();
                downButtons[7].Text = "Down";
                downButtons[7].Dock = DockStyle.Fill;
                downButtons[7].Click += (sender, e) => controller.HandleDownButtonPress(floor);
                callPanel.Controls.Add(new Label(), 0, 7);
                callPanel.Controls.Add(downButtons[7], 1, 7);
            }
            else if (i == -1)
            {
                // Only up button for bottom floor
                upButtons[0] = new Button();
                upButtons[0].Text = "Up";
                upButtons[0].Dock = DockStyle.Fill;
                upButtons[0].Click += (sender, e) => controller.HandleUpButtonPress(floor);
                callPanel.Controls.Add(upButtons[0], 0, 8);
                callPanel.Controls.Add(new Label(), 1, 8);
            }
            else
            {
                // Both buttons for other floors
                upButtons[i + 1] = new Button();
                upButtons[i + 1].Text = "Up";
                upButtons[i + 1].Dock = DockStyle.Fill;
                upButtons[i + 1].Click += (sender, e) => controller.HandleUpButtonPress(floor);
                callPanel.Controls.Add(upButtons[i + 1], 0, 7 - i);

                downButtons[i] = new Button();
                downButtons[i].Text = "Down";
                downButtons[i].Dock = DockStyle.Fill;
                downButtons[i].Click += (sender, e) => controller.HandleDownButtonPress(floor);
                callPanel.Controls.Add(downButtons[i], 1, 7 - i);
            }
        }

        callGroup.Controls.Add(callPanel);
        mainPanel.Controls.Add(callGroup);
        this.Controls.Add(mainPanel);

        // Weight control panel
        Panel weightPanel = new Panel();
        weightPanel.Dock = DockStyle.Bottom;
        weightPanel.Height = 50;
        
        weightPanel.Controls.Add(new Label() { Text = "Current Weight (kg): ", Dock = DockStyle.Left });
        
        weightField = new TextBox();
        weightField.Width = 100;
        weightField.Dock = DockStyle.Left;
        weightPanel.Controls.Add(weightField);
        
        Button weightButton = new Button();
        weightButton.Text = "Set Weight";
        weightButton.Dock = DockStyle.Left;
        weightButton.Click += (sender, e) => 
        {
            if (double.TryParse(weightField.Text, out double weight))
            {
                controller.SetCurrentWeight(weight);
                UpdateView();
            }
            else
            {
                MessageBox.Show("Please enter a valid number", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        };
        weightPanel.Controls.Add(weightButton);
        
        this.Controls.Add(weightPanel);

        // Move button
        Button moveButton = new Button();
        moveButton.Text = "Move Elevator";
        moveButton.Dock = DockStyle.Right;
        moveButton.Width = 100;
        moveButton.Click += (sender, e) => 
        {
            controller.MoveElevator();
            UpdateView();
        };
        this.Controls.Add(moveButton);
    }

    public void UpdateView()
    {
        currentFloorLabel.Text = $"Current Floor: {controller.GetCurrentFloor()}";
        
        string status;
        if (controller.IsOverweight())
        {
            status = "Status: Overweight (Cannot move)";
        }
        else
        {
            switch (controller.GetDirection())
            {
                case 1: status = "Status: Moving Up"; break;
                case -1: status = "Status: Moving Down"; break;
                default: status = "Status: Stationary"; break;
            }
        }
        statusLabel.Text = status;
    }
}