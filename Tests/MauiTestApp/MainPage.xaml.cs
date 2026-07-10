using Dapper;
using SQLite;

namespace MauiTestApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void ContentPage_Loaded(object sender, EventArgs e)
    {
        var dataSource = "Test.db";

#if ANDROID
        dataSource = Path.Combine(FileSystem.Current.AppDataDirectory, dataSource);
#endif

        try
        {
            SQLitePCL.Batteries.Init();
            using var connection = new SQLiteConnection(new SQLiteConnectionString(":memory:"));
            var sqliteVersion = connection.ExecuteScalar<string>("SELECT sqlite_version();");
            var cipher = connection.ExecuteScalar<string>("PRAGMA cipher;");
            var providerName = SQLitePCL.raw.GetNativeLibraryName();
            VersionLbl.Text = $"sqlite_version={sqliteVersion}, default cipher={cipher ?? "<null>"}, provider={providerName}";
        }
        catch (Exception ex)
        {
            VersionLbl.Text = ex.ToString();
        }
    }
}

