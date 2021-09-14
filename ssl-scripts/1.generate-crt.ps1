# https://docs.microsoft.com/en-us/dotnet/core/additional-tools/self-signed-certificates-guide

# Copy crt to client

$PARENT="grpc-example.com"
$conf = "
[req]
default_bits= 4096
distinguished_name=req
x509_extension = v3_ca
req_extensions = v3_req

[v3_req]
basicConstraints = CA:FALSE
keyUsage = nonRepudiation, digitalSignature, keyEncipherment
subjectAltName = @alt_names

[alt_names]
DNS.1 = www.$PARENT
DNS.2 = $PARENT

[v3_ca]
subjectKeyIdentifier=hash
authorityKeyIdentifier=keyid:always,issuer
basicConstraints = critical, CA:TRUE, pathlen:0
keyUsage = critical, cRLSign, keyCertSign
extendedKeyUsage = serverAuth, clientAuth
";

$file = New-TemporaryFile
Add-Content $file $conf

# $file = "C:\files\open\file2.txt"
# New-Item $file -ItemType File -Value "The first sentence in our file."
#Add-Content $file "The second sentence in our file."

openssl req `
-x509 `
-newkey rsa:4096 `
-sha256 `
-days 365 `
-nodes `
-keyout "$PARENT.key" `
-out "$PARENT.crt" `
-subj "/CN=${PARENT}" `
-extensions v3_ca `
-extensions v3_req `
-config $file

openssl x509 -noout -text -in $PARENT.crt

Remove-Item $file