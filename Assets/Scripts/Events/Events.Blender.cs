using System;

public partial class Events
{
    public static Action OnStartMixing;
    public static Action<Fruct> OnFruitAdd;
    public static Action<Fruct> OnFruitClicked;
    public static Action OnCoctailFinished;
}
