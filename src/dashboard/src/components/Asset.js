import React, { useEffect, useState } from "react";

import {
  Row,
  Col,
  Button,
  Card,
  Collapse,
  InputGroup,
  Badge,
} from "react-bootstrap";

const Asset = ({ name, onAssetStateChange }) => {
  const [open, setOpen] = useState(false);
  const [notifications, setNotifications] = useState({
    loaded: false,
    email: false,
    slack: false,
    sms: false,
  });
  const [monitorActive, setMonitor] = useState({
    loaded: false,
    isRunning: false,
  });

  useEffect(() => {
    if (!monitorActive.loaded && open) setMonitorStatus();
    if (!notifications.loaded && open) setNotificationState();
  }, [open, monitorActive.loaded, notifications.loaded]);

  const deleteAsset = async () => {
    await fetch(`http://localhost:5005/api/v1/assets/${name}`, {
      method: "DELETE",
    });
    onAssetStateChange();
  };

  const setNotificationState = async () => {
    let notResult = await fetch(
      `http://localhost:5005/api/v1/notifications/${name}`,
      {
        method: "GET",
      }
    );
    let notification = await notResult.json();
    setNotifications({
      email: notification.email,
      slack: notification.slack,
      sms: notification.sms,
    });
  };

  const setMonitorStatus = async () => {
    let monitors = await fetch(
      `http://localhost:5005/api/v1/monitors/${name}`,
      {
        method: "GET",
      }
    );
    var monitor = await monitors.text();
    setMonitor({ loaded: true, isRunning: monitor === "true" });
  };

  const changeMonitorState = async () => {
    let headers = new Headers();
    headers.set("Content-Type", "application/json");
    await fetch(
      `http://localhost:5005/api/v1/monitors/${
        monitorActive.isRunning ? "stop" : "start"
      }`,
      {
        method: "POST",
        headers: headers,
        body: JSON.stringify({ Name: name }),
      }
    );
    setMonitor({ loaded: true, isRunning: !monitorActive.isRunning });
  };

  const changeNotificationState = async (newNotifications) => {
    let headers = new Headers();
    headers.set("Content-Type", "application/json");
    await fetch(`http://localhost:5005/api/v1/notifications/${name}`, {
      method: "POST",
      headers: headers,
      body: JSON.stringify({
        Email: newNotifications.email,
        Sms: newNotifications.sms,
        Slack: newNotifications.slack,
      }),
    });
    setNotifications(newNotifications);
  };
  return (
    <>
      <Card className="mt-3">
        <Card.Body>
          <Row>
            <Col sm={10} className="d-flex align-items-center">
              <Badge bg={monitorActive.isRunning ? "success" : "secondary"}>
                {monitorActive.isRunning ? "Running" : "Stopped"}
              </Badge>
              <span className="ms-3">{name}</span>
            </Col>
            <Col sm={2}>
              <Button
                className={"ms-1 float-end"}
                onClick={() => setOpen(!open)}
                variant={open ? "warning" : "primary"}
              >
                {open ? "Close" : "Open"}
              </Button>
            </Col>
            <Collapse in={open}>
              <div>
                <Row className="mt-3">
                  <Col>
                    <span className="me-3">Notifications:</span>
                    <Badge
                      bg={notifications.email ? "success" : "secondary"}
                      className="me-3"
                      onClick={() =>
                        changeNotificationState({
                          ...notifications,
                          email: !notifications.email,
                        })
                      }
                    >
                      Email {notifications.email ? "Active" : "Inactive"}
                    </Badge>
                    <Badge
                      bg={notifications.sms ? "success" : "secondary"}
                      className="me-3"
                      onClick={() =>
                        changeNotificationState({
                          ...notifications,
                          sms: !notifications.sms,
                        })
                      }
                    >
                      SMS {notifications.sms ? "Active" : "Inactive"}
                    </Badge>
                    <Badge
                      bg={notifications.slack ? "success" : "secondary"}
                      onClick={() =>
                        changeNotificationState({
                          ...notifications,
                          slack: !notifications.slack,
                        })
                      }
                    >
                      Slack {notifications.slack ? "Active" : "Inactive"}
                    </Badge>
                  </Col>
                </Row>
                <Row className="mt-3">
                  <Col>
                    <Button
                      variant={
                        monitorActive.isRunning ? "secondary" : "success"
                      }
                      onClick={() => changeMonitorState()}
                    >
                      {monitorActive.isRunning ? "Stop" : "Start"} monitoring
                    </Button>
                    <Button
                      variant="danger"
                      className={"float-end"}
                      onClick={() => deleteAsset()}
                      disabled={monitorActive.isRunning}
                    >
                      Delete asset
                    </Button>
                  </Col>
                </Row>
              </div>
            </Collapse>
          </Row>
        </Card.Body>
      </Card>
    </>
  );
};

export default Asset;
