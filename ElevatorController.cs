public class ElevatorController
{
    private ElevatorModel model;
    private ElevatorView view;

    public ElevatorController(ElevatorModel model, ElevatorView view)
    {
        this.model = model;
        this.view = view;
    }

    public void SetView(ElevatorView view)
    {
        this.view = view;
    }

    public void HandleFloorButtonPress(int floor)
    {
        model.PressFloorButton(floor);
        view.UpdateView();
    }

    public void HandleUpButtonPress(int floor)
    {
        model.PressUpButton(floor);
        view.UpdateView();
    }

    public void HandleDownButtonPress(int floor)
    {
        model.PressDownButton(floor);
        view.UpdateView();
    }

    public void SetCurrentWeight(double weight)
    {
        model.SetCurrentWeight(weight);
        view.UpdateView();
    }

    public void MoveElevator()
    {
        model.Move();
        view.UpdateView();
    }

    public int GetCurrentFloor()
    {
        return model.GetCurrentFloor();
    }

    public int GetDirection()
    {
        return model.GetDirection();
    }

    public bool IsOverweight()
    {
        return model.IsOverweight();
    }
}