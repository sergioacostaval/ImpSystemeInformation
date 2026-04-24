(function () {
    const chartData = window.resultsChartData;

    if (!chartData || typeof Chart === "undefined") {
        return;
    }

    const {
        labels,
        closePrices,
        smaValues,
        emaValues,
        rsiValues,
        smaSignals,
        emaSignals,
        rsiSignals
    } = chartData;

    const rsi30Line = labels.map(() => 30);
    const rsi70Line = labels.map(() => 70);

    const chartTextColor = "#f8fafc";
    const chartGridColor = "rgba(148, 163, 184, 0.15)";
    const chartBuyColor = "#22c55e";
    const chartSellColor = "#ef4444";

    function buildSignalDatasets(signalArray, buyValue, sellValue, sourceData, yAxisId = "y") {
        const buyData = signalArray.map((signal, index) => signal === buyValue ? sourceData[index] : null);
        const sellData = signalArray.map((signal, index) => signal === sellValue ? sourceData[index] : null);

        return {
            buyDataset: {
                label: buyValue,
                type: "line",
                data: buyData,
                yAxisID: yAxisId,
                showLine: false,
                pointRadius: 7,
                pointHoverRadius: 9,
                pointStyle: "triangle",
                pointRotation: 0,
                pointBackgroundColor: chartBuyColor,
                pointBorderColor: chartBuyColor
            },
            sellDataset: {
                label: sellValue,
                type: "line",
                data: sellData,
                yAxisID: yAxisId,
                showLine: false,
                pointRadius: 7,
                pointHoverRadius: 9,
                pointStyle: "triangle",
                pointRotation: 180,
                pointBackgroundColor: chartSellColor,
                pointBorderColor: chartSellColor
            }
        };
    }

    function getCommonOptions(titleText) {
        return {
            responsive: true,
            interaction: {
                mode: "index",
                intersect: false
            },
            plugins: {
                legend: {
                    labels: {
                        color: chartTextColor
                    }
                },
                title: {
                    display: true,
                    text: titleText,
                    color: chartTextColor
                }
            },
            scales: {
                x: {
                    ticks: {
                        color: chartTextColor
                    },
                    grid: {
                        color: chartGridColor
                    }
                },
                y: {
                    beginAtZero: false,
                    ticks: {
                        color: chartTextColor
                    },
                    grid: {
                        color: chartGridColor
                    }
                }
            }
        };
    }

    const smaSignalDatasets = buildSignalDatasets(smaSignals, "Buy", "Sell", closePrices);
    const smaCtx = document.getElementById("smaChart");

    if (smaCtx) {
        new Chart(smaCtx, {
            type: "line",
            data: {
                labels,
                datasets: [
                    {
                        label: "Close Price",
                        data: closePrices,
                        borderWidth: 2,
                        pointRadius: 0,
                        tension: 0.25
                    },
                    {
                        label: "SMA",
                        data: smaValues,
                        borderWidth: 2,
                        pointRadius: 0,
                        tension: 0.25
                    },
                    smaSignalDatasets.buyDataset,
                    smaSignalDatasets.sellDataset
                ]
            },
            options: getCommonOptions("Price vs SMA with Buy and Sell Signals")
        });
    }

    const emaSignalDatasets = buildSignalDatasets(emaSignals, "Buy EMA", "Sell EMA", closePrices);
    const emaCtx = document.getElementById("emaChart");

    if (emaCtx) {
        new Chart(emaCtx, {
            type: "line",
            data: {
                labels,
                datasets: [
                    {
                        label: "Close Price",
                        data: closePrices,
                        borderWidth: 2,
                        pointRadius: 0,
                        tension: 0.25
                    },
                    {
                        label: "EMA",
                        data: emaValues,
                        borderWidth: 2,
                        pointRadius: 0,
                        tension: 0.25
                    },
                    emaSignalDatasets.buyDataset,
                    emaSignalDatasets.sellDataset
                ]
            },
            options: getCommonOptions("Price vs EMA with Buy and Sell Signals")
        });
    }

    const rsiSignalDatasets = buildSignalDatasets(rsiSignals, "Buy RSI", "Sell RSI", closePrices, "yPrice");
    const rsiCtx = document.getElementById("rsiChart");

    if (rsiCtx) {
        new Chart(rsiCtx, {
            type: "line",
            data: {
                labels,
                datasets: [
                    {
                        label: "Close Price",
                        data: closePrices,
                        yAxisID: "yPrice",
                        borderWidth: 2,
                        pointRadius: 0,
                        tension: 0.25
                    },
                    {
                        label: "RSI",
                        data: rsiValues,
                        yAxisID: "yRsi",
                        borderWidth: 2,
                        pointRadius: 0,
                        tension: 0.25
                    },
                    {
                        label: "RSI 30",
                        data: rsi30Line,
                        yAxisID: "yRsi",
                        borderWidth: 1,
                        pointRadius: 0,
                        borderDash: [5, 5]
                    },
                    {
                        label: "RSI 70",
                        data: rsi70Line,
                        yAxisID: "yRsi",
                        borderWidth: 1,
                        pointRadius: 0,
                        borderDash: [5, 5]
                    },
                    rsiSignalDatasets.buyDataset,
                    rsiSignalDatasets.sellDataset
                ]
            },
            options: {
                responsive: true,
                interaction: {
                    mode: "index",
                    intersect: false
                },
                plugins: {
                    legend: {
                        labels: {
                            color: chartTextColor
                        }
                    },
                    title: {
                        display: true,
                        text: "Price and RSI with Thresholds",
                        color: chartTextColor
                    }
                },
                scales: {
                    x: {
                        ticks: {
                            color: chartTextColor
                        },
                        grid: {
                            color: chartGridColor
                        }
                    },
                    yPrice: {
                        type: "linear",
                        position: "left",
                        beginAtZero: false,
                        ticks: {
                            color: chartTextColor
                        },
                        grid: {
                            color: chartGridColor
                        },
                        title: {
                            display: true,
                            text: "Price",
                            color: chartTextColor
                        }
                    },
                    yRsi: {
                        type: "linear",
                        position: "right",
                        min: 0,
                        max: 100,
                        ticks: {
                            color: chartTextColor
                        },
                        grid: {
                            drawOnChartArea: false
                        },
                        title: {
                            display: true,
                            text: "RSI",
                            color: chartTextColor
                        }
                    }
                }
            }
        });
    }
})();
