import http from 'k6/http';
import { check } from 'k6';

export let options = {
    stages: [
        { duration: '1m', target: 100 },  // Ramp up to 100 virtual users over 1 minute
        { duration: '3m', target: 100 },  // Stay at 100 virtual users for 3 minutes
        { duration: '1m', target: 0 },    // Ramp down to 0 virtual users over 1 minute
    ],
};

export default function () {
    let response = http.get('http://62.169.21.165:5002/currencyconverter');

    check(response, {
        'status is 200': (r) => r.status === 200,
    });
}
