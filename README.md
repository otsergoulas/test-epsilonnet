# Notes

Entity framework uses SqLite provider. In order to migrate:
- Open Package Manager Console
- Add-Migration InitialCreate
- update-database
- You can open db via https://sqlitebrowser.org/

Employee and Manager task is  in BlazorApp.Server -> Misc/Misc.cs

Overall Blazor seemed convenient overall, frontent development speed is signifigantly faster than i am used to.

# Todos

Interview was in Wednesday, Thursday was pretty busy so i had only half Friday to occupy myself with the test.
That said, here are some things need improvement:
- Validation in customer model
- API versioning / openapi / swagger
- UI stuff (eg. edit and create customer could be a modal / loaders)
- Didn't have the time to implement Identity authentication
