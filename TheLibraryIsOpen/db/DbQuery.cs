/*
    The DdQuery class contains methods for performing sql queries on the db.
 */
public class DbQuery
{
    public void CreateClient(string firstName, string lastName, string email, string address, string phone, string password, bool isAdmin, int id)
    {
        // TODO
    }

    public void DeleteClient(int id)
    {
        // TODO
    }

    public void SetClientName(int id, string firstName, string lastName)
    {
        // TODO
    }

    public void SetClientAddress(int id, string address)
    {
        // TODO
    }

    public void SetClientEmail(int id, string email)
    {
        // TODO
    }

    public void SetClientPhone(int id, string phone)
    {
        // TODO
    }

    public void SetClientPassword(int id, string password)
    {
        // TODO
    }

    public void SetClientId(int idOld, string idNew)
    {
        // TODO
    }

    public void setClientAdmin(int id, bool isAdmin)
    {
        // TODO
    }

    public string GetClientName(int id)
    {
        // TODO
        return "";
    }

    public string GetClientAddress(int id)
    {
        // TODO
        return "";
    }

    public string GetClientEmail(int id)
    {
        // TODO
        return "";
    }

    public string GetClientPhone(int id)
    {
        // TODO
        return "";
    }

    public string GetClientPassword(int id)
    {
        // TODO
        return "";
    }

    public int GetClientId(string firstName, string lastName) // not sure about this one
    {
        // TODO
        return 0;
    }

    public int GetClientAdmin(int id)
    {
        // TODO
        return 0;
    }

}