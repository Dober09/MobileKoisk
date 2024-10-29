# Mobile Kiosk Contribution Guidelines

## Table of Contents
1. [Introduction](#introduction)
2. [Project Structure](#project-structure)
3. [Commit Conventions](#commit-conventions)
4. [Development Workflow](#development-workflow)
5. [Testing Requirements](#testing-requirements)
6. [Pull Request Process](#pull-request-process)

## Introduction

Welcome to Mobile Kiosk! We're building a .NET MAUI application that transforms mobile devices into accessible retail self-service stations. This guide will help you contribute effectively to our project.

## Project Structure

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

### Module Guidelines

#### Services Module
```csharp
public interface IKioskService
{
    Task InitializeAsync();
    Task<Session> StartSessionAsync();
}
```

#### Views Module
```xaml
<ContentPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui">
    <Grid RowDefinitions="Auto,*,Auto">
        <!-- Implement accessibility -->
        <Label SemanticProperties.HeadingLevel="Level1"/>
    </Grid>
</ContentPage>
```

[More module details in Module Guidelines section...]

## Commit Conventions

### Message Structure
```
<type>(<scope>): <subject>

[optional body]

[optional footer(s)]
```

### MAUI-Specific Types

| Type | Usage | Example |
|------|-------|---------|
| `ui` | UI/XAML changes | `ui(checkout): redesign payment flow` |
| `layout` | Layout changes | `layout(cart): improve responsiveness` |
| `bind` | Data binding | `bind(scanner): update barcode binding` |
| `platform` | Platform-specific | `platform(ios): fix navigation` |

### Standard Types

| Type | Usage | Example |
|------|-------|---------|
| `feat` | Features | `feat(payment): add Apple Pay` |
| `fix` | Bug fixes | `fix(cart): resolve count issue` |
| `docs` | Documentation | `docs(readme): update setup` |
| `style` | Formatting | `style(views): format XAML` |

### Scope Categories

```
UI Layer:
(pages)         - XAML pages
(controls)      - Custom controls
(styles)        - Style resources

Features:
(scanner)       - Barcode scanning
(cart)          - Shopping cart
(payment)       - Payment processing

Platform:
(android)       - Android-specific
(ios)           - iOS-specific
(windows)       - Windows-specific
```

### Example Commits

```bash
# UI Changes
ui(pages): update checkout layout
- Improve spacing for accessibility
- Add semantic properties
- Update control styles

# ViewModel Changes
bind(viewmodels): implement cart validation
- Add ObservableProperty
- Include validation logic
- Update commands

# Platform-Specific
platform(ios): customize payment sheet
- Add native UI components
- Handle safe areas
- Implement iOS-specific animations
```

## Development Workflow

1. **Fork and Clone**
```bash
git clone https://github.com/your-username/mobile-kiosk.git
cd mobile-kiosk
```

2. **Set Up Development Environment**
```bash
dotnet workload install maui
dotnet restore
```

3. **Create Feature Branch**
```bash
# Format: feature/scope/description
git checkout -b feature/payment/apple-pay-integration
```

4. **Make Changes Following Conventions**
```bash
# Make changes
# Stage changes
git add .

# Commit with conventional message
git commit -m "feat(payment): implement Apple Pay integration

- Add payment handler
- Implement UI components
- Add platform-specific code

Closes #123"
```

5. **Run Tests**
```bash
dotnet test
```

## Testing Requirements

### Unit Tests
```csharp
[TestClass]
public class PaymentServiceTests
{
    [TestMethod]
    public async Task ProcessPayment_ValidAmount_Succeeds()
    {
        // Arrange
        var service = new PaymentService();
        
        // Act
        var result = await service.ProcessPaymentAsync(100);
        
        // Assert
        Assert.IsTrue(result.Success);
    }
}
```

### UI Tests
```csharp
[TestClass]
public class CheckoutPageTests
{
    [TestMethod]
    public async Task PriceDisplay_UpdatesCorrectly()
    {
        // Test UI elements
    }
}
```

## Pull Request Process

1. **Ensure Tests Pass**
```bash
dotnet test
```

2. **Update Documentation**
- Update README if needed
- Add feature documentation
- Update API documentation

3. **Create Pull Request**
- Use PR template
- Reference issues
- Add screenshots for UI changes
- List tested platforms

4. **PR Template**
```markdown
## Description
Brief description of changes

## Type of Change
- [ ] UI/XAML Change
- [ ] Feature Addition
- [ ] Bug Fix
- [ ] Documentation Update

## Tested On
- [ ] Android
- [ ] iOS
- [ ] Windows

## Screenshots
[If applicable]

## Checklist
- [ ] Tests Added/Updated
- [ ] Documentation Updated
- [ ] Commit Messages Follow Convention
- [ ] Code Follows Style Guide
```

## Style Guide Integration

### Visual Studio Settings
```json
{
    "git.enableCommitSigning": true,
    "git.allowForcePush": false,
    "editor.formatOnSave": true
}
```

### Git Hooks
```bash
#!/bin/sh
# .githooks/commit-msg

commit_msg_file=$1
commit_msg=$(cat "$commit_msg_file")

# Validate commit message format
pattern='^(ui|layout|bind|platform|feat|fix|docs|style)\(([a-z-]+)\): .+'

if ! echo "$commit_msg" | grep -Eq "$pattern"; then
    echo "Invalid commit message format"
    exit 1
fi
```

## Getting Help

- Documentation: `/docs`
- Discord: #mobile-kiosk
- Email: contribute@mobilekiosk.com

## Recognition

Contributors are recognized through:
- Contributors list in README
- Feature credits
- Community highlights

---

Remember: Quality over quantity. Take time to understand the conventions and ask questions when needed.

Need help? Contact the maintainers:
- GitHub: @mobile-kiosk-team
- Email: lebohanglehutjo@gmail.ac.ul.za
