import React from "react";

import { Row, Col, Button, Card } from "react-bootstrap";

const Asset = ({ name, lastPrice, monitoredUntil }) => {
  return (
    <>
      <Card className="mt-3">
        <Card.Body>
          <Row>
            <Col sm={10} className="d-flex align-items-center">
              {name} | {lastPrice}$ | {monitoredUntil}
            </Col>
            <Col sm={2}>
              <Button className={"ms-1 float-end"} variant="success">
                Start
              </Button>
            </Col>
          </Row>
        </Card.Body>
      </Card>
    </>
  );
};

export default Asset;
