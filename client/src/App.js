import { useState } from 'react';
import settings from '../src/settings.json';
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {

  const [url, setUrl] = useState('');

  const getNewUrl = () => {
    const recivedUrl = document.getElementById('linkInput').value;

    if (recivedUrl.trim() == '') { return; }

    const data = new FormData();
    data.append('recivedUrl', `${recivedUrl}`);

    fetch(`${settings.serviceHost}/api/Main/ShortUrl/`,
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

            if (url.GeneratedUrl) {
              setUrl(url.GeneratedUrl);
            }
            else {
              setUrl('');
            }
          });
        }
        else {
          console.log("Ошибка HTTP: " + response.status);
        }
      });
  }

  const copy = () => {
    const result = document.getElementById('resultInput');
    if (result.value.trim() == '') { return; }

    result.select();
    document.execCommand('copy');

    const span = document.getElementById('successCopySpan');
    span.classList.remove('d-none');
    span.classList.add('d-block');

    setTimeout(() => {
      span.classList.remove('d-block');
      span.classList.add('d-none');
    }, 5000);

  }

  return (
    <>
      <div className='row h-100 w-100 mt-5'>
        <div className='col-lg-3'></div>
        <div className='col-12 col-lg-6 h-100 bg-light'>
          <h3 className='mt-5 mb-1'>Кликер:)</h3>
          <span className='text-secondary mb-3'>Ссылка даётся на одни сутки с момента создания</span>

          <div className="input-group mb-3 mt-3">
            <input id='linkInput' type="text" className="form-control" placeholder="https://mycite.com" aria-describedby="linkButton" />
            <button className="btn btn-warning" onClick={getNewUrl} type="button" id="linkButton" >{'Получить ссылку  '}</button>
          </div>

          <div className="input-group mb-3">
            <input id='resultInput' type="text" readOnly value={url} className="form-control" placeholder="https://short-url/" aria-describedby="copyButton" />
            <button id='copyButton' type="button" className="btn btn-secondary" onClick={copy} data-toggle="tooltip" data-bs-placement="bottom" title="Копировать" data-delay="500">Копировать ссылку</button>
          </div>
          <span id='successCopySpan' className='text-success mb-3 d-none'>Успешно скопирован!</span>

        </div>
        <div className='col-lg-3'></div>
      </div>
    </>
  );
}

export default App;
