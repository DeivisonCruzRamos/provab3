#!/bin/bash
# Autor: Diego Fontes
# Data: 2021-03-10
# Descrição: Script para instalar pacotes nuget na imagem docker.
# Versão: 1.0
# Nota: O script deve ser coloca na pasta onde será executado o build da imagem docker.

#Instala pacotes necessários para o build
apt update
apt install -y gnupg ca-certificates vim openssh-client 
apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF 
echo "deb https://download.mono-project.com/repo/ubuntu stable-focal main" | tee /etc/apt/sources.list.d/mono-official-stable.list
apt update 

apt install -y mono-devel
curl -o /usr/local/bin/nuget.exe https://dist.nuget.org/win-x86-commandline/latest/nuget.exe

echo "----[Pacotes instalados com sucesso!]----"

#Baixa os pacotes nuget do projeto
mkdir -p /root/.ssh/
echo "$SSH_KEY" > /root/.ssh/id_rsa
chmod -R 600 /root/.ssh/
ssh-keyscan -t rsa github.com >> ~/.ssh/known_hosts
git clone $GIT_REPO_NAME /tmp/nuget

#Adiciona os pacotes nuget do projeto ao feed local
DIR="/tmp/nuget"
find "$DIR"  -name "*.nupkg" | while read line; do
  arquivo="$line"
  echo "$arquivo"
  /usr/bin/mono /usr/local/bin/nuget.exe add "$arquivo" -Source /root/.nuget/NuGet/
done

#Instala os pacotes nuget no server
DIR="/root/.nuget/NuGet/"
ls -I *.Config "$DIR" | while read line; do
  arquivo="$line"
  echo "$DIR$arquivo"
  dotnet nuget add source "$DIR$arquivo" --name "$arquivo"
done