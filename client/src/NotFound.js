import { useState } from 'react';
import settings from '../src/settings.json';
import 'bootstrap/dist/css/bootstrap.min.css';

function NotFound() {
      const [text, setText] = useState('Подождите секундочку...');
      const generateUrl = window.location.href;
      const data = new FormData();
      data.append('generateUrl', generateUrl);

      fetch(`${settings.serviceHost}/api/Main/LongUrl/`,
            {
                  mode: 'cors',
                  method: 'POST',
                  headers: {
                        Accept: 'application/json',
                  },
                  body: data
            }).then(response => {
                  if (response.ok) {

                        response.json().then(json => {
                              const url = JSON.parse(json);

                              if (url.RecivedUrl) {
                                    window.location.href = url.RecivedUrl;
                              }
                              else {
                                    setText('Ошибка 404 ,страница не найдена!');
                              }
                        });
                  }
                  else {
                        console.log("Ошибка HTTP: " + response.status);
                  }
            });


      return (
            <>
                  <h1 className='text-center mt-5'>{text}</h1>
            </>
      );
}

export default NotFound;