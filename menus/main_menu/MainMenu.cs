using Godot;

public partial class MainMenu : Node
{
    private Logger logger;

    public override void _Ready()
    {
        logger = new Logger(this);
        logger.info("Main menu!");
    }

    public void _on_exit_pressed()
    {
        logger.info("Exit pressed!");
        GetTree().Quit();
    }
}