# Dockerfile para servir o site estático em HTML/CSS/JS usando Nginx
FROM nginx:stable-alpine
LABEL maintainer="project-team"

# Copia configuração customizada do Nginx e os arquivos estáticos
COPY nginx.conf /etc/nginx/conf.d/default.conf
RUN rm -rf /usr/share/nginx/html/*
COPY . /usr/share/nginx/html

# Expor porta padrão do Nginx
EXPOSE 80

# Mantém o Nginx em primeiro plano
CMD ["nginx", "-g", "daemon off;"]
