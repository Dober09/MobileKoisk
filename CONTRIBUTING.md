# Contributing to Mobile Kiosk - Module Guide

## Table of Contents
- [Module Structure](#module-structure)
- [Contribution Guidelines by Module](#contribution-guidelines-by-module)
- [Code Standards](#code-standards)
- [Testing Requirements](#testing-requirements)
- [Pull Request Process](#pull-request-process)

## Module Structure

```
MobileKiosk/
├── Services/              # Business logic and service implementations
├── Models/                # Data models and entities
├── Views/                 # XAML UI pages
├── ViewModels/            # MVVM view models
├── Utilities/             # Helper classes and extensions
├── Resources/             # Shared resources and assets
├── Config/                # Configuration files
└── Tests/                 # Test projects
```

## Contribution Guidelines by Module

### 1. Services Module

Location: `Services/`
Purpose: Core business logic implementation

```csharp
// Example of a well-structured service
public interface IKioskService
{
    Task InitializeAsync();
    Task<Session> StartSessionAsync();
    Task EndSessionAsync(Session session);
}

public class KioskService : IKioskService
{
    private readonly ILogger<KioskService> _logger;
    
    public KioskService(ILogger<KioskService> logger)
    {
        _logger = logger;
    }
    
    public async Task InitializeAsync()
    {
        _logger.LogInformation("Initializing kiosk service");
        // Implementation
    }
}
```

Key Guidelines:
- Always implement interfaces for services
- Use dependency injection
- Include logging
- Handle exceptions appropriately
- Document public methods

### 2. Models Module

Location: `Models/`
Purpose: Data structures and entities

```csharp
public class CartItem
{
    public string ItemId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    
    public decimal Total => Price * Quantity;
}
```

Key Guidelines:
- Keep models simple and focused
- Use data annotations for validation
- Implement INotifyPropertyChanged where needed
- Include XML documentation
- Use appropriate data types

### 3. Views Module

Location: `Views/`
Purpose: XAML UI definitions

```xaml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             x:Class="MobileKiosk.Views.CheckoutPage">
    
    <Grid RowDefinitions="Auto,*,Auto"
          Padding="16">
          
        <!-- Accessibility-focused controls -->
        <Label Text="Checkout"
               SemanticProperties.HeadingLevel="Level1"
               AutomationProperties.IsInAccessibleTree="True"/>
               
        <CollectionView Grid.Row="1"
                        ItemsSource="{Binding CartItems}">
            <!-- Item template -->
        </CollectionView>
    </Grid>
</ContentPage>
```

Key Guidelines:
- Use semantic properties for accessibility
- Follow MVVM pattern
- Implement responsive layouts
- Use styles and themes
- Include automation properties

### 4. ViewModels Module

Location: `ViewModels/`
Purpose: View logic and data binding

```csharp
public partial class CheckoutViewModel : BaseViewModel
{
    private readonly ICartService _cartService;
    private readonly IPaymentService _paymentService;

    [ObservableProperty]
    private ObservableCollection<CartItem> cartItems;

    [RelayCommand]
    private async Task ProcessCheckoutAsync()
    {
        try
        {
            IsBusy = true;
            await _paymentService.ProcessPaymentAsync(GetTotal());
        }
        finally
        {
            IsBusy = false;
        }
    }
}
```

Key Guidelines:
- Use source generators ([ObservableProperty], [RelayCommand])
- Implement proper error handling
- Keep view models focused
- Use async/await properly
- Include loading states

### 5. Utilities Module

Location: `Utilities/`
Purpose: Helper classes and extensions

```csharp
public static class BarcodeScanner
{
    public static async Task<string> ScanBarcodeAsync(ICamera camera)
    {
        // Implementation
    }
}

public static class Extensions
{
    public static string ToFormattedPrice(this decimal price)
    {
        return price.ToString("C");
    }
}
```

Key Guidelines:
- Keep utilities focused and reusable
- Document usage examples
- Include unit tests
- Handle edge cases
- Use static classes appropriately

### 6. Resources Module

Location: `Resources/`
Purpose: Shared resources and assets

```xaml
<!-- Styles.xaml -->
<ResourceDictionary>
    <Style x:Key="PrimaryButton" TargetType="Button">
        <Setter Property="HeightRequest" Value="44" />
        <Setter Property="Padding" Value="16,8" />
    </Style>
</ResourceDictionary>
```

Key Guidelines:
- Use consistent naming conventions
- Organize resources by type
- Include accessibility considerations
- Optimize images and assets
- Document resource usage

### 7. Config Module

Location: `Config/`
Purpose: Application configuration

```json
{
  "ApiSettings": {
    "BaseUrl": "https://api.mobilekiosk.com",
    "Timeout": 30
  },
  "Features": {
    "EnableOfflineMode": true,
    "EnableAnalytics": true
  }
}
```

Key Guidelines:
- Use environment-specific configs
- Secure sensitive information
- Document configuration options
- Include validation
- Use strong typing

### 8. Tests Module

Location: `Tests/`
Purpose: Test projects and files

```csharp
[TestClass]
public class KioskServiceTests
{
    private readonly IKioskService _kioskService;
    
    [TestMethod]
    public async Task Initialize_WithValidConfig_Succeeds()
    {
        // Arrange
        var config = new KioskConfig();
        
        // Act
        await _kioskService.InitializeAsync(config);
        
        // Assert
        Assert.IsTrue(_kioskService.IsInitialized);
    }
}
```

Key Guidelines:
- Follow AAA pattern (Arrange, Act, Assert)
- Use meaningful test names
- Include unit and integration tests
- Test edge cases
- Maintain high coverage

## Pull Request Process

1. **Branch Naming**
```bash
feature/module-name/feature-description
bug/module-name/bug-description
```

2. **Commit Messages**
```bash
# Format:
[Module] Brief description

# Example:
[Services] Add offline payment processing
[Views] Improve checkout page accessibility
```

3. **PR Template Requirements**
- Module(s) affected
- Changes description
- Testing performed
- Screenshots (for UI changes)
- Accessibility considerations

## Code Review Checklist

### General
- [ ] Follows coding standards
- [ ] Properly documented
- [ ] Includes tests
- [ ] Handles errors appropriately

### Module-Specific
- [ ] Services: Implements interfaces
- [ ] Models: Includes validation
- [ ] Views: Accessibility compliant
- [ ] ViewModels: Follows MVVM
- [ ] Tests: Covers edge cases

## Getting Help

- Check module-specific documentation in `/docs`
- Join our Discord channel
- Tag maintainers in PR comments
- Review existing implementations

## Recognition

Contributors are recognized through:
- Module maintainer roles
- Feature credits
- Documentation acknowledgments
- Community highlights

Need help? Contact: contribute@mobilekiosk.com
