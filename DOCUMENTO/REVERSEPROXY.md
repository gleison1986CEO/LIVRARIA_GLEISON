# HOW TO MAKE PROXY REVERSE (APACHE2)

- sudo a2enmod proxy
- sudo a2enmod proxy_http
- sudo a2enmod proxy_balancer
- sudo a2enmod lbmethod_byrequests   
- sudo systemctl restart apache2     
---    

## APACHE FILES
- cd /etc/apache2/sites-available/
- nano SSL & nano default
----


## MODIFIED FILES LIKE THIS ABOVE
- ESSES CAMPOS ABAIXO PRECISAM DO IP E DA PORTA DO SERVIDOR REAL QUE SERA UTILIZADO.

<VirtualHost *:80>
    ProxyPreserveHost On
    
    ProxyPass / http://127.0.0.1:8080/
    ProxyPassReverse / http://127.0.0.1:8080/
</VirtualHost>
----
<VirtualHost *:443>
    ProxyPreserveHost On

    ProxyPass / http://127.0.0.1:8080/
    ProxyPassReverse / http://127.0.0.1:8080/
</VirtualHost>
----

## DONE
- DONE
----

