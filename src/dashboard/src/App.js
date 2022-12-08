import React, { useEffect, useState } from "react";
import { Container, Row, Col, Spinner } from "react-bootstrap";
import Asset from "./components/Asset";
import AddNewAssetForm from "./components/AddNewAssetForm";

function App() {
  const [assets, setAssets] = useState([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    let fetchData = async () => {
      var response = await fetch("http://localhost:5005/api/v1/assets", {
        method: "GET",
      });
      var result = await response.json();
      var storedAssets = result.map((a) => ({
        name: a.name,
        lastPrice: 123.312,
        monitoredUntil: new Date().toLocaleDateString(),
      }));
      setAssets(storedAssets);
      setIsLoading(false);
    };
    fetchData();
  }, [isLoading]);

  return (
    <Container className="mt-3">
      <h1 className="text-center">NQuadro dashboard</h1>
      <Row>
        <Col sm={6} className="mt-3">
          <h2>Your current assets:</h2>
          {isLoading && <Spinner animation="border" />}
          {!isLoading && assets.map((a) => <Asset {...a} key={a.name} />)}
        </Col>
        <Col sm={1} />
        <Col sm={5} className="mt-3">
          <AddNewAssetForm onNewAssetAdded={() => setIsLoading(true)} />
        </Col>
      </Row>
    </Container>
  );
}

export default App;
