export default function Tags() {
  const http = require("http"); //Web server
  const express = require("express");
  const app = express();
  const {Server} = require("socket.io");
  const server = http.createServer(app);
  const io = new Server(server);
  const SerialPort = require("serialPort"); // Node SerialPort
  const portName = process.argv[2] || "COM4";
  const port = new SerialPort(portName, {
    baudRate: 9600,
    dataBits: 8,
    parity: "none",
    stopBits: 1,
    flowControl: false,
  });
  const ReadlineParser = require("@serialport/parser-readline");
  const parser = port.pipe(new ReadlineParser({delimiter: "\r\n"}));
  parser.on("data", (data) => {
    io.emit("arduino message", data);
    port.write("asdf\n");
  });
  port.on("open", () => {});

  const PORT = process.env.PORT || 5000;
  app.get("/", (req, res) => {
    res.sendFile(__dirname + "/index.html");
  });
  io.on("connection", (socket) => {
    socket.on("chat message", (msg) => {
      io.emit("chat message", msg);
    });
  });
  server.listen(PORT, () => console.log("Server is working"));
}
