#https://docs.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-5.0
#Copy pfx to server cert

$PARENT="grpc-example.com"
openssl pkcs12 -export -out "$PARENT.pfx" -inkey "$PARENT.key" -in "$PARENT.crt"