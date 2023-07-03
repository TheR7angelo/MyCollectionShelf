using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using MyCollectionShelf.Wpf.Shelf.Common.Class;

namespace MyCollectionShelf.Wpf.Shelf.Common.Ui.Controls;

public partial class LanguageControl
{
    public List<Language> Languages { get; }

    public LanguageControl()
    {
        InitializeComponent();

        var availableCultures = GetAvailableCultures();
        Languages = availableCultures.Select(cultureInfo => new Language
        {
            CultureInfo = cultureInfo,
            DisplayName = LanguageControlResources.ResourceManager.GetString(cultureInfo.EnglishName)!,
            Name = cultureInfo.Name
        }).ToList();
    }

    private static IEnumerable<CultureInfo> GetAvailableCultures()
    {
        // Création du gestionnaire de ressources
        var resourceManager = new ResourceManager(typeof(LanguageControlResources));

        // Obtention des informations relatives aux cultures prises en charge
        var supportedCultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
            .Where(s => !s.Equals(CultureInfo.InvariantCulture));
        var availableCultures = new List<CultureInfo>();
        availableCultures.AddRange(supportedCultures.Where(culture =>
            resourceManager.GetResourceSet(culture, true, false) != null));

        return availableCultures;
    }
}