using System.Collections.Generic;

public class ElevatorModel
{
    private int currentFloor;
    private bool[] floorButtons; // Internal buttons
    private bool[] upButtons;    // External up buttons
    private bool[] downButtons;  // External down buttons
    private Queue<int> destinationFloors;
    private int direction; // 1 - up, -1 - down, 0 - stationary
    private double currentWeight;
    private const double MAX_WEIGHT = 700.0;
    private bool isOverweight;

    public ElevatorModel()
    {
        currentFloor = 0;
        floorButtons = new bool[9]; // -1 to 7
        upButtons = new bool[8];    // -1 to 6 (can't go up from 7)
        downButtons = new bool[8];  // 0 to 7 (can't go down from -1)
        destinationFloors = new Queue<int>();
        direction = 0;
        currentWeight = 0;
        isOverweight = false;
    }

    public void SetCurrentWeight(double weight)
    {
        this.currentWeight = weight;
        this.isOverweight = weight > MAX_WEIGHT;
    }

    public bool IsOverweight()
    {
        return isOverweight;
    }

    public void PressFloorButton(int floor)
    {
        if (floor >= -1 && floor <= 7)
        {
            floorButtons[floor + 1] = true;
            AddDestination(floor);
        }
    }

    public void PressUpButton(int floor)
    {
        if (floor >= -1 && floor <= 6)
        {
            upButtons[floor + 1] = true;
            AddDestination(floor);
        }
    }

    public void PressDownButton(int floor)
    {
        if (floor >= 0 && floor <= 7)
        {
            downButtons[floor] = true;
            AddDestination(floor);
        }
    }

    private void AddDestination(int floor)
    {
        if (!destinationFloors.Contains(floor))
        {
            destinationFloors.Enqueue(floor);
        }
    }

    public void Move()
    {
        if (isOverweight)
        {
            return; // Elevator doesn't move when overweight
        }

        if (destinationFloors.Count == 0)
        {
            direction = 0;
            return;
        }

        int nextFloor = destinationFloors.Peek();
        
        if (nextFloor > currentFloor)
        {
            direction = 1;
            currentFloor++;
        }
        else if (nextFloor < currentFloor)
        {
            direction = -1;
            currentFloor--;
        }
        else
        {
            // Reached the destination floor
            direction = 0;
            destinationFloors.Dequeue();
            floorButtons[currentFloor + 1] = false;
            
            if (currentFloor >= 0 && currentFloor <= 6)
            {
                upButtons[currentFloor + 1] = false;
            }
            if (currentFloor >= 0 && currentFloor <= 7)
            {
                downButtons[currentFloor] = false;
            }
        }
    }

    public int GetCurrentFloor()
    {
        return currentFloor;
    }

    public int GetDirection()
    {
        return direction;
    }

    public bool[] GetFloorButtons()
    {
        return floorButtons;
    }

    public bool[] GetUpButtons()
    {
        return upButtons;
    }

    public bool[] GetDownButtons()
    {
        return downButtons;
    }
}