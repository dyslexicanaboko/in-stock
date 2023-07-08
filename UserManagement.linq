<Query Kind="Program">
  <Connection>
    <ID>203a5e2a-fb01-4ef4-9934-763dddf526f6</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Database>InStock</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
  <NuGetReference>BCrypt.Net-Next</NuGetReference>
  <Namespace>BCrypt.Net</Namespace>
  <IncludeUncapsulator>false</IncludeUncapsulator>
</Query>

void Main()
{
	var pw = HashPassword("emmC2YNvh%9LtNMHWo#T");

	$"UPDATE dbo.[User] SET Password = '{pw}' WHERE UserId = 0".Dump();
}

// You can define other methods, fields, classes and namespaces here
private static string HashPassword(string plainTextPassword)
{
	var salt = BCrypt.Net.BCrypt.GenerateSalt(12);

	var hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainTextPassword, salt);

	return hashedPassword;
}