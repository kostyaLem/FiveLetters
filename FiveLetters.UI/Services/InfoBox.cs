using HandyControl.Controls;
using HandyControl.Data;

namespace FiveLetters.UI.Services;

/// <summary>
/// Статический класс с подготовленными диалоговыми окнами
/// для отображения результатов игры
/// </summary>
internal static class InfoBox
{
    public static void ShowWin()
    {
        MessageBox.Show(new()
        {
            Message = "Угадали",
            ConfirmContent = "Закрыть",
            IconBrushKey = ResourceToken.SuccessBrush,
            IconKey = ResourceToken.SuccessGeometry
        });
    }

    public static void ShowLose()
    {
        MessageBox.Show(new()
        {
            Message = "Не угадали. Все попытки потрачены.",
            ConfirmContent = "Закрыть",
            IconBrushKey = ResourceToken.AccentBrush,
            IconKey = ResourceToken.ErrorGeometry
        });
    }

    public static void ShowEnd()
    {
        MessageBox.Show(new()
        {
            Message = "Игра пройдена. Отгаданы все слова.",
            ConfirmContent = "Закрыть",
            IconBrushKey = ResourceToken.SuccessBrush,
            IconKey = ResourceToken.SuccessGeometry
        });
    }
}
