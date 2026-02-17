import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  vus: 200,          // Virtual users
  duration: '30s', // Test duration
};

export default function () {
  const res = http.get('http://localhost:5116/SdccvJ4');

  check(res, {
    'status is 200': (r) => r.status === 200,
  });

  sleep(1);
}
