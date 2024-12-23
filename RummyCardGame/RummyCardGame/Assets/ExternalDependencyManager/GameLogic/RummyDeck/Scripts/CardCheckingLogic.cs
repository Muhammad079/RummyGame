public static class CardCheckingLogic
{
  public static  bool CheckNumberMatch(CardView first, CardView current, CardView second)
    {
        if (first.card.no == current.card.no && second.card.no == current.card.no)
        {

            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool CheckDeckMatch(CardView first, CardView current, CardView second)
    {
        if (first.Carddec == current.Carddec && second.Carddec == current.Carddec)
        {
            if (first.card.no == current.card.no + 1 && second.card.no == current.card.no + 2)
            {
                return true;
            }
            else if (first.card.no == current.card.no - 1 && second.card.no == current.card.no - 2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}