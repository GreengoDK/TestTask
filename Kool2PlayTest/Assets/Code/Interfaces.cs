//for implementing player input
public interface IPlayerInputImplementable
{
    void OnPlayerInput(InputMap playerInput);
}
//for implementing custom update
public interface IUpdateable
{
    void CustomUpdate();
}
//for implementing shooting in different weapons
public interface IShooting
{
    void Shoot();
}



