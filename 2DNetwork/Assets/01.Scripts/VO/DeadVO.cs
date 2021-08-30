[System.Serializable]
public class DeadVO 
{
    public int socketId;
    public int killerId;

    public DeadVO(int soc, int killerId)
    {
        socketId = soc;
        this.killerId = killerId;
    }
}
