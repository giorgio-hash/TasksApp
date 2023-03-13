
#developement stage
FROM node:13.13.0-alpine as build-stage
RUN addgroup app && adduser --system app
RUN addgroup app app 
USER app
WORKDIR /app
COPY package*.json ./
RUN npm install
COPY . .
RUN npm run build

# production stage
FROM nginx:stable-alpine as production-stage
COPY --from=build-stage /app/dist /usr/share/nginx/html
COPY /nginx/nginx.conf  /etc/nginx/conf.d/default.conf
WORKDIR /usr/share/nginx/html
EXPOSE 8080
