import http from 'k6/http';
import { check } from 'k6';

export let options = {
    stages: [
        { duration: '10s', target: 50 },
        { duration: '1m', target: 50 },
        { duration: '10s', target: 650 },
        { duration: '3m', target: 650 },
        { duration: '10s', target: 50 },
        { duration: '3m', target: 50 },
        { duration: '10s', target: 600 },
        { duration: '3m', target: 600 },
        { duration: '10s', target: 50 },
        { duration: '3m', target: 50 },
        { duration: '10s', target: 0 },
    ],
};

export default function () {
    let response = http.get('http://62.169.21.165:5002/currencyconverter');

    check(response, {
        'status is 200': (r) => r.status === 200,
    });
}