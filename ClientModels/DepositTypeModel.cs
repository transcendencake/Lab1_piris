namespace Lab1_piris.ClientModels;

public class DepositTypeModel: SelectedItemModel
{
    public decimal Percent { get; set; }

    public bool IsRecallable { get; set; }
}