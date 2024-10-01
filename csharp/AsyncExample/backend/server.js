const express = require('express');
const fs = require('fs').promises; // Use promises to read the JSON file
const path = require('path');

const app = express();
const PORT = 3000;
const HOST = '0.0.0.0';

// Middleware to add a delay
app.use((req, res, next) => {
  setTimeout(() => {
    next();
  }, 2000); // 2000ms = 2 seconds delay
});

// Serve static JSON data from db.json
app.get('/data', async (req, res) => {
  try {
    const dataPath = path.join(__dirname, 'db.json'); // Ensure db.json is in the same directory
    const data = await fs.readFile(dataPath, 'utf8');
    res.json(JSON.parse(data)); // Parse and send the JSON data
  } catch (error) {
    res.status(500).send('Error reading data');
  }
});

// Start the server
app.listen(PORT, HOST, () => {
  console.log(`Server running at http://${HOST}:${PORT}`);
});
