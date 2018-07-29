using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Timers;

namespace OneCharter.Timing {
    public class HexaTimer {
        private Stopwatch stopwatch;
        private Timer timer;
        private bool isRunning;

        public double Interval { get; set; }
        public double StartTime { get; set; }
        public bool IsRunning {
            get => isRunning;
            set {
                if (value == isRunning) return;
                if (value) Start();
                else Stop();
            }
        }

        public HexaTimer(double interval = 1000d/60) {
            Interval = interval;

            stopwatch = new Stopwatch();
            timer = new Timer(interval);
            timer.Elapsed += OnTimerTick;
            timer.AutoReset = false;
            isRunning = false;
        }
        
        public void Start(double startTime = 0) {
            if (isRunning) {
                Stop();
            }
            StartTime = startTime;
            isRunning = true;
            timer.Interval = Interval;
            timer.Start();
            stopwatch.Restart();
        }

        public void Stop() {
            if (!isRunning) return;
            stopwatch.Reset();
            timer.Stop();
            isRunning = false;
        }

        private void OnTimerTick(object sender, ElapsedEventArgs args) {
            if (!isRunning) return;

            HexaTickEventArgs hexaArgs = new HexaTickEventArgs();
            long elapsed = stopwatch.ElapsedMilliseconds;
            double currentTime = StartTime + elapsed;

            hexaArgs.CurrentTime = currentTime;
            hexaArgs.Elapsed = elapsed;

            Tick?.Invoke(this, hexaArgs);

            // It is possible that one of event handlers disabled this timer.
            if (!isRunning) return;
            timer.Interval = Interval;
            timer.Start();
        }

        public event EventHandler<HexaTickEventArgs> Tick;
    }

    public class HexaTickEventArgs: EventArgs {
        public double CurrentTime;
        public long Elapsed;
    }
}
