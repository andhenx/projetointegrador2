# Frontend — Docker

Este diretório contém um site estático (HTML/CSS/JS) e um `Dockerfile` para servi-lo com Nginx.

Requisitos

- Docker instalado

Build da imagem

```bash
docker build -t projeto-frontend:latest -f frontend/Dockerfile frontend
```

Executar o container (mapeando para a porta 8080)

```bash
docker run --rm -p 8080:80 projeto-frontend:latest
```

Acesse: http://localhost:8080

Observações

- O `Dockerfile` copia `nginx.conf` custom em `frontend/nginx.conf` e serve `index.html` com fallback (útil para SPAs).
- Há um `.dockerignore` para evitar arquivos desnecessários na imagem.
- Para desenvolvimento local, você pode usar uma extensão de servidor estático (ex.: Live Server) sem Docker.

Parar o container

- Se o container foi iniciado em primeiro plano (sem `-d`), pressione `Ctrl+C` no terminal onde o `docker run` está em execução.
- Se o container foi iniciado em background (ou com `-d`), pare-o assim:

```bash
docker ps
docker stop <CONTAINER_ID or NAME>
```

- Dica: inicie o container com um nome para facilitar a parada:

```bash
docker run --rm -p 8080:80 --name projeto-frontend projeto-frontend:latest
docker stop projeto-frontend
```

- Para remover a imagem localmente (opcional):

```bash
docker rmi projeto-frontend:latest
```
