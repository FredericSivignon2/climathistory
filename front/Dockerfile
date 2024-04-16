# Étape 1: Construire l'application React
FROM node:18-alpine as build

# Définir le répertoire de travail dans le conteneur
WORKDIR /app

# Copier les fichiers de dépendances et installer les dépendances
COPY package*.json ./
RUN npm install

# Copier le reste des fichiers de l'application
COPY . .

# Construire l'application pour la production
RUN npm run build

# Étape 2: Préparer l'image de production avec nginx
FROM nginx:alpine

# Copier les fichiers de construction depuis l'étape de build vers le répertoire de travail de nginx
COPY --from=build /app/dist /usr/share/nginx/html

# Exposer le port 80 pour l'application
EXPOSE 80

# Lancer nginx en mode foreground
CMD ["nginx", "-g", "daemon off;"]
