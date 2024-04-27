import { Selector } from 'testcafe';

fixture `Count Conversions`
    .page `http://62.169.21.165:5001/`;

test('Count Conversions', async t => {
    // Make a request to the Converter API to get the number of conversions
    const conversionAPIResponse = await t
        .request("http://62.169.21.165:5002/currencyconverter");

    // Extract the number of conversions from the API response
    const conversionCount = conversionAPIResponse.body.length;

    // Count the number of rows in the history table on the Converter Frontend page
    const tableRowCount = Selector("table#history tbody tr").count;

    // Assert that the number of conversions matches the number of rows in the table
    await t.expect(tableRowCount).eql(conversionCount);
});
